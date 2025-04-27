using InventoryPlus.Domain;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;

namespace InventoryPlus.Infrastructure.Repositories;

public class ConsumableCategoryRepository : BaseRepository<ConsumableCategory>, IConsumableCategoryRepository
{
    public ConsumableCategoryRepository(InventoryContext context) : base(context)
    {
    }
}