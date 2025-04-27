using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryPlus.Domain.Entities;

/// <summary>
/// Представляет модель оборудования в системе
/// </summary>
public class EquipmentModel
{
    /// <summary>
    /// Уникальный идентификатор модели оборудования
    /// </summary>
    [Key]
    public Guid ModelId { get; set; }
        
    /// <summary>
    /// Идентификатор типа оборудования
    /// </summary>
    [ForeignKey("EquipmentType")]
    public Guid TypeId { get; set; }
    
    /// <summary>
    /// Ссылка на тип оборудования
    /// </summary>
    [JsonIgnore]
    public EquipmentType Type { get; set; }
        
    /// <summary>
    /// Наименование модели оборудования
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
        
    /// <summary>
    /// Производитель оборудования
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Manufacturer { get; set; }
}