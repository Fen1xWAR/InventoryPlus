using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using InventoryPlus.Domain.Entities;

namespace InventoryPlus.Domain.DTO;

public class EquipmentInstanceDto
{
    public Guid ModelId { get; set; }


    public string SerialNumber { get; set; }


    public string InventoryNumber { get; set; }


    public Guid CabinetId { get; set; }


    public EquipmentStatus Status { get; set; } = EquipmentStatus.New;

    public DateTime InstallationDate { get; set; } = DateTime.UtcNow;
}