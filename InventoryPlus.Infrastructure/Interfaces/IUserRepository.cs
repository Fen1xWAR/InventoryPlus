using InventoryPlus.Domain.Entities;

namespace InventoryPlus.Infrastructure.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetUserByEmail(string username);
}