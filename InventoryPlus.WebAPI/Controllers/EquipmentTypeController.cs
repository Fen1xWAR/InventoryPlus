using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryPlus.Domain;
using InventoryPlus.Domain.DTO;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure;
using InventoryPlus.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryPlus.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EquipmentTypeController : ControllerBase
    {
        private readonly IEquipmentTypeRepository _equipmentRepository;

        public EquipmentTypeController(IEquipmentTypeRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _equipmentRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentType>> GetById(Guid id)
        {
            var equipmentType = await _equipmentRepository.GetByIdAsync(id);
            if (equipmentType == null) return NotFound();
            return Ok(equipmentType);
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentType>> Insert(EquipmentTypeDto equipmentTypeDto)
        {
            var equipmentType = new EquipmentType()
            {
                Name = equipmentTypeDto.Name,
                TypeId = Guid.NewGuid(),
            };
            await _equipmentRepository.AddAsync(equipmentType);
            return CreatedAtAction(nameof(GetById), new { id = equipmentType.TypeId }, equipmentType);
        }

        [HttpPost]
        public async Task<ActionResult<EquipmentType>> Update(EquipmentType equipmentType)
        {
            if (!await _equipmentRepository.ExistsAsync(e => e.TypeId.Equals(equipmentType.TypeId))) return NotFound();
            await _equipmentRepository.UpdateAsync(equipmentType);
            return CreatedAtAction(nameof(GetById), new { id = equipmentType.TypeId }, equipmentType);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<EquipmentType>> DeleteById(Guid id)
        {
            var equipmentType = await _equipmentRepository.GetByIdAsync(id);
            if (equipmentType == null) return NotFound();
            await _equipmentRepository.DeleteAsync(equipmentType);
            return NoContent();
        }
    }
}