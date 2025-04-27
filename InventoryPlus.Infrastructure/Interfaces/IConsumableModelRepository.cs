using InventoryPlus.Domain;
using InventoryPlus.Domain.Entities;

namespace InventoryPlus.Infrastructure.Interfaces;

public interface IConsumableModelRepository : IRepository<ConsumableModel>
{
    Task<IEnumerable<ConsumableModel>> GetConsumablesWithCategoriesByIds(IEnumerable<Guid> ids);
}