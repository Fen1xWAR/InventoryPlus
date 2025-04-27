
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;

namespace InventoryPlus.Infrastructure.Repositories
{
    public class EquipmentTypeRepository : BaseRepository<EquipmentType>, IEquipmentTypeRepository
    {
        public EquipmentTypeRepository(InventoryContext context) : base(context)
        {
        }
    }
}