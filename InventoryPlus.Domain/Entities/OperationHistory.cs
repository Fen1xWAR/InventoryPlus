using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryPlus.Domain.Entities;

/// <summary>
/// Представляет историю операций в системе
/// </summary>
public class OperationHistory
{
    /// <summary>
    /// Уникальный идентификатор записи истории
    /// </summary>
    [Key]
    public Guid HistoryId { get; set; }
        
    /// <summary>
    /// Тип сущности, с которой производилась операция
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string EntityType { get; set; }
        
    /// <summary>
    /// Идентификатор сущности, с которой производилась операция
    /// </summary>
    [Required]
    public Guid EntityId { get; set; }
        
    /// <summary>
    /// Тип выполненной операции
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string ActionType { get; set; }
        
    /// <summary>
    /// Идентификатор пользователя, выполнившего операцию
    /// </summary>
    [ForeignKey("User")]
    public Guid? UserId { get; set; }
    
    /// <summary>
    /// Ссылка на пользователя, выполнившего операцию
    /// </summary>
    [JsonIgnore]
    public User User { get; set; }
        
    /// <summary>
    /// Предыдущий статус оборудования (если операция связана с оборудованием)
    /// </summary>
    public EquipmentStatus? OldStatus { get; set; }
    
    /// <summary>
    /// Новый статус оборудования (если операция связана с оборудованием)
    /// </summary>
    public EquipmentStatus? NewStatus { get; set; }
        
    /// <summary>
    /// Комментарий к операции
    /// </summary>
    public string Comment { get; set; }
        
    /// <summary>
    /// Дата и время выполнения операции
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}