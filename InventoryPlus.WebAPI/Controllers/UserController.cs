using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Acro.Domain.AuthModels;
using InventoryPlus.Domain;
using InventoryPlus.Domain.AuthModels;
using InventoryPlus.Domain.DTO;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;
using InventoryPlus.WebAPI.Controllers.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryPlus.WebAPI.Controllers
{
    /// <summary>
    /// Контроллер для управления пользователями системы
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthService _authService;
        
        public UserController(IUserRepository userRepository, AuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

       
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _userRepository.GetAllAsync());
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

       
        [HttpPost]
        public async Task<ActionResult<User>> Insert(UserDto userDto)
        {
            var user = new User()
            {
                UserId = Guid.NewGuid(),
                Email = userDto.Email,
                PasswordHash = userDto.PasswordHash,
                Role = userDto.Role,
                CreatedAt = userDto.CreatedAt,
            };
            await _userRepository.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

       
        [HttpPost]
        public async Task<ActionResult<User>> Update(User user)
        {
            if (!await _userRepository.ExistsAsync(e => e.UserId.Equals(user.UserId))) return NotFound();
            await _userRepository.UpdateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

       
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();
            await _userRepository.DeleteAsync(user);
            return NoContent();
        }

       
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var result = await _authService.Register(registerModel);
            if (result == "")
                return BadRequest("Пользователь с таким email уже существует");
            return Ok(result);
        }

       
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
        {
            var result = await _authService.Login(loginModel);
            if (result == "Пол")
                return BadRequest("Неверное имя пользователя или пароль");
            return Ok(result);
        }
    }
}
