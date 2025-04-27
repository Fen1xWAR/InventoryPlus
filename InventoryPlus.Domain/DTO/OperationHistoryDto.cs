using InventoryPlus.Domain.Entities;

namespace InventoryPlus.Domain.DTO;

public class OperationHistoryDto
{

        

    public string EntityType { get; set; }
        

    public Guid EntityId { get; set; }
        


    public string ActionType { get; set; }
        

    public Guid? UserId { get; set; }

        
    public EquipmentStatus? OldStatus { get; set; }
    public EquipmentStatus? NewStatus { get; set; }
        
    public string Comment { get; set; }
        
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}