using Frutti.Server.Domain.Entities;

namespace Frutti.Server.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByUsernameAsync(string username);

    Task AddUserAsync(User user);

    Task UpdateUserAsync(User user);
}
