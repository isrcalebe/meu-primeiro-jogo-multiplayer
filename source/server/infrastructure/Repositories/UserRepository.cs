using System;
using System.Threading.Tasks;
using Frutti.Server.Domain.Entities;
using Frutti.Server.Domain.Repositories;

namespace Frutti.Server.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public Task<User?> GetUserByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetUserByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public Task AddUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
