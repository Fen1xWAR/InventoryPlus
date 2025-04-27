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
        protected readonly InventoryContext _context;
        protected readonly DbSet<T> _dbSet;
        
        public BaseRepository(InventoryContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        
        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}