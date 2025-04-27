using InventoryPlus.Domain;
using InventoryPlus.Domain.Entities;

namespace InventoryPlus.Infrastructure.Interfaces;

public interface IEquipmentModelRepository : IRepository<EquipmentModel>
{
    Task<IEnumerable<ConsumableModel>> GetConsumablesWithCategoriesAsync(Guid equipmentModelId);

}