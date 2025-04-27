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
        public DbSet<User> Users { get; set; }
        
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<Cabinet> Cabinets { get; set; }
   
        public DbSet<ConsumableCategory> ConsumableCategories { get; set; }
        
        public DbSet<ConsumableModel> ConsumableModels { get; set; }
       
        public DbSet<Consumable> Consumables { get; set; }
        
        public DbSet<EquipmentType> EquipmentTypes { get; set; }
        
        public DbSet<EquipmentModel> EquipmentModels { get; set; }
  
        public DbSet<EquipmentInstance> EquipmentInstances { get; set; }
        
        public DbSet<OperationHistory> OperationHistories { get; set; }

       
        /// <param name="modelBuilder">Построитель модели данных</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связей для EquipmentConsumable
            modelBuilder.Entity<EquipmentConsumable>()
                .HasKey(ec => new { ec.EquipmentModelId, ec.ConsumableModelId });
        }
    }
}