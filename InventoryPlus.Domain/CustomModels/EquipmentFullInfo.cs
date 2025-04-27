using InventoryPlus.Domain.Entities;

namespace InventoryPlus.Domain.CustomModels;

public class EquipmentFullInfo
{
    public string Type { get; set; }
    public string Model { get; set; }
    public string SerialNumber { get; set; }
    public string InventoryNumber { get; set; }
    public Dictionary<string,IEnumerable<ConsumableModel>> Consumables { get; set; }
    public EquipmentStatus Status { get; set; }
    public DateTime InstallationDate { get; set; }
    public CabinetFullInfo Cabinet { get; set; }
}