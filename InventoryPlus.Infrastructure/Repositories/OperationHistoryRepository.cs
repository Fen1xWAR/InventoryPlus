using InventoryPlus.Domain;
using InventoryPlus.Domain.DTO;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;

namespace InventoryPlus.Infrastructure.Repositories
{
    public class OperationHistoryRepository : BaseRepository<OperationHistory>, IOperationHistoryRepository
    {
        public OperationHistoryRepository(InventoryContext context) : base(context)
        {
        }
    }
}