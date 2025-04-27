using System.ComponentModel.DataAnnotations;

namespace InventoryPlus.Domain.Entities;

/// <summary>
/// Представляет сущность пользователя системы
/// </summary>
public class User
{
    /// <summary>
    /// Уникальный идентификатор пользователя
    /// </summary>
    [Key]
    public Guid UserId { get; set; }
        
    /// <summary>
    /// Электронная почта пользователя
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Email { get; set; }
        
    /// <summary>
    /// Хеш пароля пользователя
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string PasswordHash { get; set; }
        
    /// <summary>
    /// Роль пользователя в системе
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string Role { get; set; }
        
    /// <summary>
    /// Дата и время создания учетной записи пользователя
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}