using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using InventoryPlus.Domain.Entities;

namespace InventoryPlus.Domain.DTO;

public class CabinetDto
{
    public string Number { get; set; }


    public Guid BuildingId { get; set; }


    public Guid? ResponsibleEmployeeId { get; set; }


    public string Description { get; set; }
}