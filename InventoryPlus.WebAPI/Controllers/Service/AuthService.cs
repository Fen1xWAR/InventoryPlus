using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Acro.Domain.AuthModels;
using InventoryPlus.Domain.AuthModels;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace InventoryPlus.WebAPI.Controllers.Service;

public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public AuthService(IConfiguration configuration, IUserRepository userRepository, IEmployeeRepository employeeRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<string> Login(LoginModel model)
    {
        try
        {
            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user == null || user.PasswordHash != model.Password) return "";
            return GenerateJwtToken(user);
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("Произошла ошибка в процессе входа");
        }
    }

    public async Task<string> Register(RegisterModel model)
    {
        try
        {
            var existingUser = await _userRepository.GetUserByEmail(model.Email);
            if (existingUser != null)
            {
               
                return "";
            }

            var newUser = new User
            {
                UserId = Guid.Empty,
                Email = model.Email,
                CreatedAt = DateTime.UtcNow,
                PasswordHash = model.Password,
                Role = model.Role,

            };
            await _userRepository.AddAsync(newUser);
            await _employeeRepository.AddAsync(new Employee
            {
                EmployeeId = Guid.Empty,
                FullName = model.Fullname,
                Position = model.Position,
                ContactInfo = model.ContactInfo,
                UserId = newUser.UserId,
            });
            return GenerateJwtToken(newUser);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new InvalidOperationException("Произошла ошибка в процессе регистрации");
        }
        
    }

    private string GenerateJwtToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}