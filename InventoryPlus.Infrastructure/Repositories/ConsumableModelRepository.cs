using InventoryPlus.Domain;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryPlus.Infrastructure.Repositories;

public class ConsumableModelRepository : BaseRepository<ConsumableModel>, IConsumableModelRepository
{
    public ConsumableModelRepository(InventoryContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ConsumableModel>> GetConsumablesWithCategoriesByIds(IEnumerable<Guid> ids)
    {
        return await _context.ConsumableModels
            .Include(c => c.Category) 
            .Where(c => ids.Contains(c.ModelId))
            .AsNoTracking()
            .ToListAsync();
    }
}