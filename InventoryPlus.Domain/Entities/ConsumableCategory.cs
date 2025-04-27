using System.ComponentModel.DataAnnotations;

namespace InventoryPlus.Domain.Entities;

/// <summary>
/// Представляет категорию расходных материалов
/// </summary>
public class ConsumableCategory
{
    /// <summary>
    /// Уникальный идентификатор категории
    /// </summary>
    [Key]
    public Guid CategoryId { get; set; }
        
    /// <summary>
    /// Наименование категории
    /// </summary>
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}
