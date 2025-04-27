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
    public class CabinetController : ControllerBase
    {
        private readonly ICabinetRepository _cabinetRepository;

        public CabinetController(ICabinetRepository cabinetRepository)
        {
            _cabinetRepository = cabinetRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _cabinetRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Cabinet>> GetById(Guid id)
        {
            var cabinet = await _cabinetRepository.GetByIdAsync(id);
            if (cabinet == null) return NotFound();
            return Ok(cabinet);
        }

        [HttpPost]
        public async Task<ActionResult<Cabinet>> Insert(CabinetDto cabinetDto)
        {
            var cabinet = new Cabinet
            {
                Number = cabinetDto.Number,
                BuildingId = cabinetDto.BuildingId,
                Description = cabinetDto.Description,
                ResponsibleEmployeeId = cabinetDto.ResponsibleEmployeeId,
                CabinetId = Guid.NewGuid(),
            };
            await _cabinetRepository.AddAsync(cabinet);
            return CreatedAtAction(nameof(GetById), new { id = cabinet.CabinetId }, cabinet);
        }

        [HttpPost]
        public async Task<ActionResult<Cabinet>> Update(Cabinet cabinet)
        {
            if (!await _cabinetRepository.ExistsAsync(e => e.CabinetId.Equals(cabinet.CabinetId))) return NotFound();
            await _cabinetRepository.UpdateAsync(cabinet);
            return CreatedAtAction(nameof(GetById), new { id = cabinet.CabinetId }, cabinet);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Cabinet>> DeleteById(Guid id)
        {
            var cabinet = await _cabinetRepository.GetByIdAsync(id);
            if (cabinet == null) return NotFound();
            await _cabinetRepository.DeleteAsync(cabinet);
            return NoContent();
        }
    }
}