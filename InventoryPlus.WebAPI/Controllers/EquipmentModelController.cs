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
    public class EquipmentModelController : ControllerBase
    {
        private readonly IEquipmentModelRepository _equipmentModelRepository;
        private readonly IEquipmentConsumableRepository _equipmentConsumableRepository;

        public EquipmentModelController(IEquipmentModelRepository equipmentModelRepository, IEquipmentConsumableRepository equipmentConsumableRepository)
        {
            _equipmentModelRepository = equipmentModelRepository;
            _equipmentConsumableRepository = equipmentConsumableRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _equipmentModelRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentModel>> GetById(Guid id)
        {
            var equipmentModel = await _equipmentModelRepository.GetByIdAsync(id);
            if (equipmentModel == null) return NotFound();
            return Ok(equipmentModel);
        }
        [HttpPost]
        public async Task<ActionResult<EquipmentModel>> Insert(EquipmentModelDto equipmentModelDto)
        {
            var equipmentModel = new EquipmentModel()
            {
                ModelId = Guid.NewGuid(),
                Manufacturer = equipmentModelDto.Manufacturer,
                Name = equipmentModelDto.Name,
                TypeId = equipmentModelDto.TypeId,
            };
            await _equipmentModelRepository.AddAsync(equipmentModel);
            return CreatedAtAction(nameof(GetById), new { id = equipmentModel.ModelId }, equipmentModel);
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentConsumable>> LinkEquipmentModelWithConsumableModel(EquipmentConsumableDto equipmentConsumableDto)
        {
            var equipmentConsumable = new EquipmentConsumable()
            {
                ConsumableModelId = equipmentConsumableDto.ConsumableModelId,
                EquipmentModelId = equipmentConsumableDto.EquipmentModelId,
            };
            await _equipmentConsumableRepository.AddAsync(equipmentConsumable);
            return Ok(equipmentConsumable);
        }
        [HttpPost]
        public async Task<ActionResult<EquipmentModel>> Update(EquipmentModel equipmentModel)
        {
            if (!await _equipmentModelRepository.ExistsAsync(e => e.ModelId.Equals(equipmentModel.ModelId))) return NotFound();
            await _equipmentModelRepository.UpdateAsync(equipmentModel);
            return CreatedAtAction(nameof(GetById), new { id = equipmentModel.ModelId }, equipmentModel);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<EquipmentModel>> DeleteById(Guid id)
        {
            var equipmentModel = await _equipmentModelRepository.GetByIdAsync(id);
            if (equipmentModel == null) return NotFound();
            await _equipmentModelRepository.DeleteAsync(equipmentModel);
            return NoContent();
        }
    }
}