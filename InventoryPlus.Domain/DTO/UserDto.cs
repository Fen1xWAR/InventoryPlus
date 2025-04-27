﻿using System.ComponentModel.DataAnnotations;

namespace InventoryPlus.Domain.DTO;

public class UserDto
{


    public string Email { get; set; }
        

    public string PasswordHash { get; set; }
        

    public string Role { get; set; }
        
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}