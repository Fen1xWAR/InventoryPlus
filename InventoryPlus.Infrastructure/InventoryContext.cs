using InventoryPlus.Domain;
using InventoryPlus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryPlus.Infrastructure
{
    /// <summary>
    /// Контекст базы данных для работы с сущностями системы
    /// </summary>
    public class InventoryContext : DbContext
    {
        /// <summary>
        /// Конструктор контекста базы данных
        /// </summary>
        /// <param name="options">Параметры конфигурации контекста</param>
        public InventoryContext(DbContextOptions<InventoryContext> options) 
            : base(options) { }

        /// <summary>
        /// Набор данных пользователей
        /// </summary>
        public DbSet<User> Users { get; set; }
        
        /// <summary>
        /// Набор данных сотрудников
        /// </summary>
        public DbSet<Employee> Employees { get; set; }
        
        /// <summary>
        /// Набор данных зданий
        /// </summary>
        public DbSet<Building> Buildings { get; set; }
        
        /// <summary>
        /// Набор данных шкафов
        /// </summary>
        public DbSet<Cabinet> Cabinets { get; set; }
        
        /// <summary>
        /// Набор данных категорий расходных материалов
        /// </summary>
        public DbSet<ConsumableCategory> ConsumableCategories { get; set; }
        
        /// <summary>
        /// Набор данных моделей расходных материалов
        /// </summary>
        public DbSet<ConsumableModel> ConsumableModels { get; set; }
        
        /// <summary>
        /// Набор данных расходных материалов
        /// </summary>
        public DbSet<Consumable> Consumables { get; set; }
        
        /// <summary>
        /// Набор данных типов оборудования
        /// </summary>
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        
        /// <summary>
        /// Набор данных моделей оборудования
        /// </summary>
        public DbSet<EquipmentModel> EquipmentModels { get; set; }
        
        /// <summary>
        /// Набор данных экземпляров оборудования
        /// </summary>
        public DbSet<EquipmentInstance> EquipmentInstances { get; set; }
        
        /// <summary>
        /// Набор данных истории операций
        /// </summary>
        public DbSet<OperationHistory> OperationHistories { get; set; }

        /// <summary>
        /// Настройка модели данных при создании
        /// </summary>
        /// <param name="modelBuilder">Построитель модели данных</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связей для EquipmentConsumable
            modelBuilder.Entity<EquipmentConsumable>()
                .HasKey(ec => new { ec.EquipmentModelId, ec.ConsumableModelId });
        }
    }
}