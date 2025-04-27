using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryPlus.Domain.Entities;

/// <summary>
/// Представляет шкаф для хранения оборудования и расходных материалов
/// </summary>
public class Cabinet
{
    /// <summary>
    /// Уникальный идентификатор шкафа
    /// </summary>
    [Key]
    public Guid CabinetId { get; set; }
        
    /// <summary>
    /// Номер шкафа
    /// </summary>
    [Required]
    [MaxLength(20)]
    public string Number { get; set; }
        
    /// <summary>
    /// Идентификатор здания, в котором находится шкаф
    /// </summary>
    [ForeignKey("Building")]
    public Guid BuildingId { get; set; }
    
    /// <summary>
    /// Ссылка на здание, в котором находится шкаф
    /// </summary>
    [JsonIgnore]
    public Building Building { get; set; }
        
    /// <summary>
    /// Идентификатор ответственного сотрудника
    /// </summary>
    [ForeignKey("Employee")]
    public Guid? ResponsibleEmployeeId { get; set; }
    
    /// <summary>
    /// Ссылка на ответственного сотрудника
    /// </summary>
    [JsonIgnore]
    public Employee ResponsibleEmployee { get; set; }
        
    /// <summary>
    /// Описание шкафа
    /// </summary>
    public string Description { get; set; }
}