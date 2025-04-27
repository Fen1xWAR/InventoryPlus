// Domain/Interfaces/IRepository.cs

using System.Linq.Expressions;

namespace InventoryPlus.Infrastructure.Interfaces
{
    /// <summary>
    /// Базовый интерфейс репозитория для работы с сущностями
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Получение сущности по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Найденная сущность</returns>
        Task<T> GetByIdAsync(Guid id);
        
        /// <summary>
        /// Получение всех сущностей
        /// </summary>
        /// <returns>Список всех сущностей</returns>
        Task<IEnumerable<T>> GetAllAsync();
        
        /// <summary>
        /// Поиск сущностей по условию
        /// </summary>
        /// <param name="predicate">Условие поиска</param>
        /// <returns>Список найденных сущностей</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        
        /// <summary>
        /// Добавление новой сущности
        /// </summary>
        /// <param name="entity">Добавляемая сущность</param>
        Task AddAsync(T entity);
        
        /// <summary>
        /// Обновление существующей сущности
        /// </summary>
        /// <param name="entity">Обновляемая сущность</param>
        Task UpdateAsync(T entity);
        
        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity">Удаляемая сущность</param>
        Task DeleteAsync(T entity);
        
        /// <summary>
        /// Проверка существования сущности по условию
        /// </summary>
        /// <param name="predicate">Условие проверки</param>
        /// <returns>True, если сущность существует, иначе False</returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}