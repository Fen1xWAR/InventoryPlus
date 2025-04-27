using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace InventoryPlus.Domain.DTO;

public class EmployeeDto
{
    public string FullName { get; set; }


    public string Position { get; set; }

    public string ContactInfo { get; set; }


    public Guid? UserId { get; set; }
}