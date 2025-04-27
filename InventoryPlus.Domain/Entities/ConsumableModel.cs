using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryPlus.Domain.Entities;

/// <summary>
/// Представляет модель расходного материала
/// </summary>
public class ConsumableModel
{
    /// <summary>
    /// Уникальный идентификатор модели расходного материала
    /// </summary>
    [Key] public Guid ModelId { get; set; }

    /// <summary>
    /// Идентификатор категории расходного материала
    /// </summary>
    [ForeignKey("ConsumableCategory")] public Guid CategoryId { get; set; }
    
    /// <summary>
    /// Ссылка на категорию расходного материала
    /// </summary>
    [JsonIgnore] public ConsumableCategory Category { get; set; }

    /// <summary>
    /// Наименование модели расходного материала
    /// </summary>
    [Required] [MaxLength(100)] public string Name { get; set; }
}