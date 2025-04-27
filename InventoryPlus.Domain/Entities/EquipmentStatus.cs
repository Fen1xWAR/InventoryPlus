namespace InventoryPlus.Domain.Entities;

/// <summary>
/// Перечисление возможных статусов оборудования
/// </summary>
public enum EquipmentStatus
{
    /// <summary>
    /// Новое оборудование
    /// </summary>
    New,
    
    /// <summary>
    /// Оборудование в использовании
    /// </summary>
    InUse,
    
    /// <summary>
    /// Сломанное оборудование
    /// </summary>
    Broken,
    
    /// <summary>
    /// Оборудование в ремонте
    /// </summary>
    UnderRepair,
    
    /// <summary>
    /// Списанное оборудование
    /// </summary>
    Disposed
}