using InventoryPlus.Domain;
using InventoryPlus.Domain.DTO;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;

namespace InventoryPlus.Infrastructure.Repositories;

public class EquipmentInstanceRepository : BaseRepository<EquipmentInstance>, IEquipmentInstanceRepository
{
    public EquipmentInstanceRepository(InventoryContext context) : base(context)
    {
    }
}