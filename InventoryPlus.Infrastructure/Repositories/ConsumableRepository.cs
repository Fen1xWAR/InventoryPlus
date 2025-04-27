using InventoryPlus.Domain;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;

namespace InventoryPlus.Infrastructure.Repositories;

public class ConsumableRepository : BaseRepository<Consumable>, IConsumableRepository
{
    public ConsumableRepository(InventoryContext context) : base(context)
    {
    }
}