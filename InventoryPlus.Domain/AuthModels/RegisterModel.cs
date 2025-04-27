using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Acro.Domain.AuthModels;

public class RegisterModel
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; }
    [Required]
    public string Fullname { get; set; }
    [Required] 
    public string ContactInfo { get; set; }
    [Required] 
    public string Position { get; set; }
}