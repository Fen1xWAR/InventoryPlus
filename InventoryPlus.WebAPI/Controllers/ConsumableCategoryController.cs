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
    public class ConsumableCategoryController : ControllerBase
    {
        private readonly IConsumableCategoryRepository _consumableCategoryRepository;

        public ConsumableCategoryController(IConsumableCategoryRepository consumableCategoryRepository)
        {
            _consumableCategoryRepository = consumableCategoryRepository;
            
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _consumableCategoryRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumableCategory>> GetById(Guid id)
        {
            var consumableCategory = await _consumableCategoryRepository.GetByIdAsync(id);
            if (consumableCategory == null) return NotFound();
            return Ok(consumableCategory);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumableCategory>> Insert(ConsumableCategoryDto consumableCategoryDto)
        {
            var consumableCategory = new ConsumableCategory
            {
                CategoryId = Guid.NewGuid(),
                Name = consumableCategoryDto.Name,
            };
            await _consumableCategoryRepository.AddAsync(consumableCategory);
            return CreatedAtAction(nameof(GetById), new { id = consumableCategory.CategoryId }, consumableCategory);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumableCategory>> Update(ConsumableCategory consumableCategory)
        {
            if (!await _consumableCategoryRepository.ExistsAsync(e => e.CategoryId.Equals(consumableCategory.CategoryId))) return NotFound();
            await _consumableCategoryRepository.UpdateAsync(consumableCategory);
            return CreatedAtAction(nameof(GetById), new { id = consumableCategory.CategoryId }, consumableCategory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ConsumableCategory>> DeleteById(Guid id)
        {
            var consumableCategory = await _consumableCategoryRepository.GetByIdAsync(id);
            if (consumableCategory == null) return NotFound();
            await _consumableCategoryRepository.DeleteAsync(consumableCategory);
            return NoContent();
        }
    }
}
