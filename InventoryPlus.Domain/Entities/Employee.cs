using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryPlus.Domain.Entities;

/// <summary>
/// Представляет сотрудника в системе
/// </summary>
public class Employee
{
    /// <summary>
    /// Уникальный идентификатор сотрудника
    /// </summary>
    [Key]
    public Guid EmployeeId { get; set; }
        
    /// <summary>
    /// Полное имя сотрудника
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }
        
    /// <summary>
    /// Должность сотрудника
    /// </summary>
    [MaxLength(100)]
    public string Position { get; set; }
        
    /// <summary>
    /// Контактная информация сотрудника
    /// </summary>
    public string ContactInfo { get; set; }
        
    /// <summary>
    /// Идентификатор пользователя, связанного с сотрудником
    /// </summary>
    [ForeignKey("User")]
    public Guid? UserId { get; set; }
    
    /// <summary>
    /// Ссылка на пользователя, связанного с сотрудником
    /// </summary>
    [JsonIgnore]
    public User User { get; set; }
}