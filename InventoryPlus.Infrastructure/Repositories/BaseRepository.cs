// Infrastructure/Repositories/BaseRepository.cs

using System.Linq.Expressions;
using InventoryPlus.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryPlus.Infrastructure
{
    /// <summary>
    /// Базовый класс репозитория, реализующий основные операции с данными
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Контекст базы данных
        /// </summary>
        protected readonly InventoryContext _context;
        
        /// <summary>
        /// Набор данных для работы с сущностью
        /// </summary>
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// Конструктор базового репозитория
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public BaseRepository(InventoryContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Получение сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Найденная сущность</returns>
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Получение всех сущностей
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Поиск сущностей по условию
        /// </summary>
        /// <param name="predicate">Условие поиска</param>
        /// <returns>Список найденных сущностей</returns>
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Добавление новой сущности
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Обновление существующей сущности
        /// </summary>
        /// <param name="entity">Обновляемая сущность</param>
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity">Удаляемая сущность</param>
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Проверка существования сущности по условию
        /// </summary>
        /// <param name="predicate">Условие проверки</param>
        /// <returns>True, если сущность существует, иначе False</returns>
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}