using System;
using System.Threading.Tasks;
using Frutti.Server.Domain.Entities;

namespace Frutti.Server.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);

    Task<User?> GetUserByUsername(string username);

    Task AddUserAsync(User user);

    Task UpdateUserAsync(User user);

    Task<bool> DeleteUserAsync(Guid id);
}
