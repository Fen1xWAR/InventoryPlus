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

        /// <summary>
        /// Конструктор контроллера пользователей
        /// </summary>
        /// <param name="userRepository">Репозиторий пользователей</param>
        /// <param name="authService">Сервис аутентификации</param>
        public UserController(IUserRepository userRepository, AuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        /// <summary>
        /// Получение списка всех пользователей
        /// </summary>
        /// <returns>Список пользователей</returns>
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            return Ok(await _userRepository.GetAllAsync());
        }

        /// <summary>
        /// Получение пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Пользователь</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Создание нового пользователя
        /// </summary>
        /// <param name="userDto">Данные пользователя</param>
        /// <returns>Созданный пользователь</returns>
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

        /// <summary>
        /// Обновление данных пользователя
        /// </summary>
        /// <param name="user">Обновленные данные пользователя</param>
        /// <returns>Обновленный пользователь</returns>
        [HttpPost]
        public async Task<ActionResult<User>> Update(User user)
        {
            if (!await _userRepository.ExistsAsync(e => e.UserId.Equals(user.UserId))) return NotFound();
            await _userRepository.UpdateAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }

        /// <summary>
        /// Удаление пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteById(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();
            await _userRepository.DeleteAsync(user);
            return NoContent();
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="registerModel">Данные для регистрации</param>
        /// <returns>Результат регистрации</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var result = await _authService.Register(registerModel);
            if (result == "")
                return BadRequest("Пользователь с таким email уже существует");
            return Ok(result);
        }

        /// <summary>
        /// Аутентификация пользователя
        /// </summary>
        /// <param name="loginModel">Данные для входа</param>
        /// <returns>JWT токен</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginModel loginModel)
        {
            var result = await _authService.Login(loginModel);
            if (result == "")
                return BadRequest();
            return Ok(result);
        }
    }
}
