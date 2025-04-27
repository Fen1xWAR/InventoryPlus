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
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingRepository _buildingRepository;

        public BuildingController(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _buildingRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetById(Guid id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);
            if (building == null) return NotFound();
            return Ok(building);
        }

        [HttpPost]
        public async Task<ActionResult<Building>> Insert(BuildingDto buildingDto)
        {
            var building = new Building
            {
                Name = buildingDto.Name,
                Address = buildingDto.Address,
                BuildingId = Guid.NewGuid()
            };
            await _buildingRepository.AddAsync(building);
            return CreatedAtAction(nameof(GetById), new { id = building.BuildingId }, building);
        }

        [HttpPost]
        public async Task<ActionResult<Building>> Update(Building building)
        {
            if (!await _buildingRepository.ExistsAsync(e => e.BuildingId.Equals(building.BuildingId))) return NotFound();
            await _buildingRepository.UpdateAsync(building);
            return CreatedAtAction(nameof(GetById), new { id = building.BuildingId }, building);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Building>> DeleteById(Guid id)
        {
            var building = await _buildingRepository.GetByIdAsync(id);
            if (building == null) return NotFound();
            await _buildingRepository.DeleteAsync(building);
            return NoContent();
        }
    }
}
