using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryPlus.Domain;
using InventoryPlus.Domain.DTO;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryPlus.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConsumableController : ControllerBase
    {
        private readonly IConsumableRepository _consumableRepository;

        public ConsumableController(IConsumableRepository consumableRepository)
        {
            _consumableRepository = consumableRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _consumableRepository.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Consumable>> GetById(Guid id)
        {
            var consumable = await _consumableRepository.GetByIdAsync(id);
            if (consumable == null) return NotFound();
            return Ok(consumable);
        }
        [HttpPost]
        public async Task<ActionResult<Consumable>> Insert(ConsumableDto consumableDto)
        {
            var consumable = new Consumable
            {
                CabinetId = consumableDto.CabinetId,
                ModelId = consumableDto.ModelId,
                VariantName = consumableDto.VariantName,
                Quantity = consumableDto.Quantity,
                ConsumableId = Guid.NewGuid(),
            };
            await _consumableRepository.AddAsync(consumable);
            return CreatedAtAction(nameof(GetById), new { id = consumable.ConsumableId }, consumable);
        }
        [HttpPost]
        public async Task<ActionResult<Consumable>> Update(Consumable consumable)
        {
            if (!await _consumableRepository.ExistsAsync(e => e.ConsumableId.Equals(consumable.ConsumableId))) return NotFound();
            await _consumableRepository.UpdateAsync(consumable);
            return CreatedAtAction(nameof(GetById), new { id = consumable.ConsumableId }, consumable);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Consumable>> DeleteById(Guid id)
        {
            var consumableCategory = await _consumableRepository.GetByIdAsync(id);
            if (consumableCategory == null) return NotFound();
            await _consumableRepository.DeleteAsync(consumableCategory);
            return NoContent();
        }
    }
}
