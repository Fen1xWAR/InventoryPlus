using InventoryPlus.Domain;
using InventoryPlus.Domain.DTO;
using InventoryPlus.Domain.Entities;

namespace InventoryPlus.Infrastructure.Interfaces;

public interface IEquipmentConsumableRepository : IRepository<EquipmentConsumable>
{ 
    Task<List<Guid>> GetAllConsumableModelIdsByEquipmentId(Guid equipmentId);
    Task<List<Guid>> GetAllEquipmentModelIdsByConsumableId(Guid consumableId);
}