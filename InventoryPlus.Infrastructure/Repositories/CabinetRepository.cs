using InventoryPlus.Domain;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;

namespace InventoryPlus.Infrastructure.Repositories;

public class CabinetRepository : BaseRepository<Cabinet>, ICabinetRepository
{
    public CabinetRepository(InventoryContext context) : base(context)
    {
    }
}