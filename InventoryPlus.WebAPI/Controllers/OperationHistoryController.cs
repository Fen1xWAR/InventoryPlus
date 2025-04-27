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
    public class OperationHistoryController : ControllerBase
    {
        private readonly IOperationHistoryRepository _operationHistoryRepository;

        public OperationHistoryController(IOperationHistoryRepository operationHistoryRepository)
        {
            _operationHistoryRepository = operationHistoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _operationHistoryRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OperationHistory>> GetById(Guid id)
        {
            var operationHistory = await _operationHistoryRepository.GetByIdAsync(id);
            if (operationHistory == null) return NotFound();
            return Ok(operationHistory);
        }

        [HttpPost]
        public async Task<ActionResult<OperationHistory>> Insert(OperationHistoryDto operationHistoryDto)
        {
            var operationHistory = new OperationHistory()
            {
                ActionType = operationHistoryDto.ActionType,
                CreatedAt = operationHistoryDto.CreatedAt,
                Comment = operationHistoryDto.Comment,
                EntityId = operationHistoryDto.EntityId,
                EntityType = operationHistoryDto.EntityType,
                HistoryId = Guid.NewGuid(),
                UserId = operationHistoryDto.UserId,
                OldStatus = operationHistoryDto.OldStatus,
                NewStatus = operationHistoryDto.NewStatus,
            };
            await _operationHistoryRepository.AddAsync(operationHistory);
            return CreatedAtAction(nameof(GetById), new { id = operationHistory.HistoryId }, operationHistory);
        }

        [HttpPost]
        public async Task<ActionResult<OperationHistory>> Update(OperationHistory operationHistory)
        {
            if (!await _operationHistoryRepository.ExistsAsync(e => e.HistoryId.Equals(operationHistory.HistoryId)))
                return NotFound();
            await _operationHistoryRepository.UpdateAsync(operationHistory);
            return CreatedAtAction(nameof(GetById), new { id = operationHistory.HistoryId }, operationHistory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OperationHistory>> DeleteById(Guid id)
        {
            var operationHistory = await _operationHistoryRepository.GetByIdAsync(id);
            if (operationHistory == null) return NotFound();
            await _operationHistoryRepository.DeleteAsync(operationHistory);
            return NoContent();
        }
    }
}