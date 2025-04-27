using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryPlus.Domain.Entities;

/// <summary>
/// Представляет экземпляр оборудования в системе
/// </summary>
public class EquipmentInstance
{
    /// <summary>
    /// Уникальный идентификатор экземпляра оборудования
    /// </summary>
    [Key]
    public Guid InstanceId { get; set; }
        
    /// <summary>
    /// Идентификатор модели оборудования
    /// </summary>
    [ForeignKey("EquipmentModel")]
    public Guid ModelId { get; set; }
    
    /// <summary>
    /// Ссылка на модель оборудования
    /// </summary>
    [JsonIgnore]
    public EquipmentModel Model { get; set; }
        
    /// <summary>
    /// Серийный номер оборудования
    /// </summary>
    [MaxLength(100)]
    public string SerialNumber { get; set; }
        
    /// <summary>
    /// Инвентарный номер оборудования
    /// </summary>
    [MaxLength(50)]
    public string InventoryNumber { get; set; }
        
    /// <summary>
    /// Идентификатор шкафа, в котором находится оборудование
    /// </summary>
    [ForeignKey("Cabinet")]
    public Guid CabinetId { get; set; }
    
    /// <summary>
    /// Ссылка на шкаф, в котором находится оборудование
    /// </summary>
    [JsonIgnore]
    public Cabinet Cabinet { get; set; }
        
    /// <summary>
    /// Текущий статус оборудования
    /// </summary>
    [Required]
    public EquipmentStatus Status { get; set; } = EquipmentStatus.New;
        
    /// <summary>
    /// Дата установки оборудования
    /// </summary>
    public DateTime InstallationDate { get; set; } = DateTime.UtcNow;
}