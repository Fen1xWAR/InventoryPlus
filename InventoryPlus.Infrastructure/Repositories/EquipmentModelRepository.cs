using InventoryPlus.Domain;
using InventoryPlus.Domain.DTO;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryPlus.Infrastructure.Repositories;

public class EquipmentModelRepository : BaseRepository<EquipmentModel>, IEquipmentModelRepository
{
    private IConsumableModelRepository _consumableModelRepository;
    private IEquipmentConsumableRepository _equipmentConsumableRepository;

    public EquipmentModelRepository(InventoryContext context, IConsumableModelRepository consumableModelRepository,
        IEquipmentConsumableRepository equipmentConsumableRepository) : base(context)
    {
        _consumableModelRepository = consumableModelRepository;
        _equipmentConsumableRepository = equipmentConsumableRepository;
    }

    public async Task<IEnumerable<ConsumableModel>> GetConsumablesWithCategoriesAsync(Guid equipmentModelId)
    {
        // 1. Получаем список идентификаторов расходников
        var consumableModelIds = await _equipmentConsumableRepository
            .GetAllConsumableModelIdsByEquipmentId(equipmentModelId);

        if (!consumableModelIds.Any())
            return Enumerable.Empty<ConsumableModel>();

        // 2. Загружаем все расходники с категориями одним запросом
        return await _consumableModelRepository
            .GetConsumablesWithCategoriesByIds(consumableModelIds);
    }
}