using System.ComponentModel.DataAnnotations;

namespace InventoryPlus.Domain.Entities;

/// <summary>
/// Представляет здание в системе
/// </summary>
public class Building
{
    /// <summary>
    /// Уникальный идентификатор здания
    /// </summary>
    [Key]
    public Guid BuildingId { get; set; }
        
    /// <summary>
    /// Наименование здания
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
        
    /// <summary>
    /// Адрес здания
    /// </summary>
    [Required]
    public string Address { get; set; }
}