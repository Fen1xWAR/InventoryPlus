using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryPlus.Domain.DTO;

public class ConsumableDto
{


    public Guid ModelId { get; set; }


    public string VariantName { get; set; }


    public Guid CabinetId { get; set; }

    [Range(0, int.MaxValue)] public int Quantity { get; set; }
}