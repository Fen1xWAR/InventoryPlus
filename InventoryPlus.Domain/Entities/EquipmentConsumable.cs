using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryPlus.Domain.Entities;

public class EquipmentConsumable
{
    [ForeignKey("EquipmentModel")]
    public Guid EquipmentModelId { get; set; }
    [JsonIgnore]
    public EquipmentModel EquipmentModel { get; set; }
        
    [ForeignKey("ConsumableModel")]
    public Guid ConsumableModelId { get; set; }
    [JsonIgnore]
    public ConsumableModel ConsumableModel { get; set; }
}