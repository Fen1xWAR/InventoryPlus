using InventoryPlus.Domain;
using InventoryPlus.Domain.DTO;
using InventoryPlus.Domain.Entities;
using InventoryPlus.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InventoryPlus.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(InventoryContext context) : base(context)
    {
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _dbSet.Where(e => e.Email == email).FirstOrDefaultAsync();
    }
}