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
       
        Task<T> GetByIdAsync(Guid id);
        
        
        Task<IEnumerable<T>> GetAllAsync();
        
       
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        
        
        Task AddAsync(T entity);
        
        
        Task UpdateAsync(T entity);
        
        
        Task DeleteAsync(T entity);
        
        
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}