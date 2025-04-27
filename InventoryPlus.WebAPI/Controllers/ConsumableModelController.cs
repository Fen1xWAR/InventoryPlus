
using InventoryPlus.Domain.DTO;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;
using InventoryPlus.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryPlus.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConsumableModelController : ControllerBase
    {
        private readonly IConsumableModelRepository _consumableModelRepository;

        public ConsumableModelController(IConsumableModelRepository consumableModelRepository)
        {
            _consumableModelRepository = consumableModelRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _consumableModelRepository.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumableModel>> GetById(Guid id)
        {
            var consumableModel = await _consumableModelRepository.GetByIdAsync(id);
            if (consumableModel == null) return NotFound();
            return Ok(consumableModel);
        }
        [HttpPost]
        public async Task<ActionResult<ConsumableModel>> Insert(ConsumableModelDto consumableModelDto)
        {
            var consumableModel = new ConsumableModel
            {
                CategoryId = consumableModelDto.CategoryId,
                Name = consumableModelDto.Name,
                ModelId = Guid.NewGuid()
            };
            await _consumableModelRepository.AddAsync(consumableModel);
            return CreatedAtAction(nameof(GetById), new { id = consumableModel.ModelId }, consumableModel);
        }
        [HttpPost]
        public async Task<ActionResult<ConsumableModel>> Update(ConsumableModel consumableModel)
        {
            if (!await _consumableModelRepository.ExistsAsync(e => e.CategoryId.Equals(consumableModel.ModelId))) return NotFound();
            await _consumableModelRepository.UpdateAsync(consumableModel);
            return CreatedAtAction(nameof(GetById), new { id = consumableModel.ModelId }, consumableModel);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ConsumableModel>> DeleteById(Guid id)
        {
            var consumableModel = await _consumableModelRepository.GetByIdAsync(id);
            if (consumableModel == null) return NotFound();
            await _consumableModelRepository.DeleteAsync(consumableModel);
            return NoContent();
        }
    }
}
