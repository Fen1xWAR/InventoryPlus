using System.ComponentModel.DataAnnotations;

namespace InventoryPlus.Domain.Entities;

/// <summary>
/// Представляет тип оборудования в системе
/// </summary>
public class EquipmentType
{
    /// <summary>
    /// Уникальный идентификатор типа оборудования
    /// </summary>
    [Key]
    public Guid TypeId { get; set; }
        
    /// <summary>
    /// Наименование типа оборудования
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}