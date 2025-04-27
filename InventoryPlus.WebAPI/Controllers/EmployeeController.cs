

using System;
using System.Threading.Tasks;
using InventoryPlus.Domain;
using InventoryPlus.Domain.DTO;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventoryPlus.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _employeeRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetById(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return NotFound();
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Insert(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                ContactInfo = employeeDto.ContactInfo,
                FullName = employeeDto.FullName,
                Position = employeeDto.Position,
                UserId = employeeDto.UserId,
                EmployeeId = Guid.NewGuid()
            };
            await _employeeRepository.AddAsync(employee);
            return CreatedAtAction(nameof(GetById), new { id = employee.EmployeeId }, employee);
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> Update(Employee employee)
        {
            if (!await _employeeRepository.ExistsAsync(e => e.EmployeeId == employee.EmployeeId))
                return NotFound();
            
            await _employeeRepository.UpdateAsync(employee);
            return Ok(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null) return NotFound();
            
            await _employeeRepository.DeleteAsync(employee);
            return NoContent();
        }
    }
}