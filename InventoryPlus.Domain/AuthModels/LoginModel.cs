using System.ComponentModel.DataAnnotations;

namespace InventoryPlus.Domain.AuthModels;

public class LoginModel
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}