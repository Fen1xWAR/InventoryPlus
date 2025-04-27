using InventoryPlus.Domain;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryPlus.Infrastructure.Repositories;

public class EquipmentConsumableRepository : BaseRepository<EquipmentConsumable>, IEquipmentConsumableRepository
{
    public EquipmentConsumableRepository(InventoryContext context) : base(context)
    {
    }


    public async Task<List<Guid>> GetAllConsumableModelIdsByEquipmentId(Guid equipmentId)
    {
        return await _dbSet
            .Where(e => e.EquipmentModelId == equipmentId)
            .Select(e => e.ConsumableModelId)
            .ToListAsync();
    }

    public async Task<List<Guid>> GetAllEquipmentModelIdsByConsumableId(Guid consumableId)
    {
        return await _dbSet
            .Where(e => e.ConsumableModelId == consumableId)
            .Select(e => e.EquipmentModelId)
            .ToListAsync();
    }
}