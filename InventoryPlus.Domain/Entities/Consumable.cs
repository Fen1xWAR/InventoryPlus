using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryPlus.Domain.Entities;

/// <summary>
/// Представляет расходный материал в системе
/// </summary>
public class Consumable
{
    /// <summary>
    /// Уникальный идентификатор расходного материала
    /// </summary>
    [Key]
    public Guid ConsumableId { get; set; }
        
    /// <summary>
    /// Идентификатор модели расходного материала
    /// </summary>
    [ForeignKey("ConsumableModel")]
    public Guid ModelId { get; set; }
    
    /// <summary>
    /// Ссылка на модель расходного материала
    /// </summary>
    [JsonIgnore]
    public ConsumableModel Model { get; set; }
        
    /// <summary>
    /// Наименование варианта расходного материала
    /// </summary>
    [Required]
    [MaxLength(100)]
    public string VariantName { get; set; }
        
    /// <summary>
    /// Идентификатор шкафа, в котором хранится расходный материал
    /// </summary>
    [ForeignKey("Cabinet")]
    public Guid CabinetId { get; set; }
    
    /// <summary>
    /// Ссылка на шкаф, в котором хранится расходный материал
    /// </summary>
    [JsonIgnore]
    public Cabinet Cabinet { get; set; }
        
    /// <summary>
    /// Количество расходного материала
    /// </summary>
    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }
}