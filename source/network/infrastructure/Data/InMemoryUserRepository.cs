using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Frutti.Server.Domain.Entities;
using Frutti.Server.Domain.Repositories;

namespace Frutti.Server.Infrastructure.Data;

public class InMemoryUserRepository : IUserRepository
{
    private static readonly ConcurrentDictionary<Guid, User> users = [];

    public Task<User?> GetUserByIdAsync(Guid id)
    {
        users.TryGetValue(id, out var user);

        return Task.FromResult(user);
    }

    public Task<User?> GetUserByUsernameAsync(string username)
    {
        var user = users.Values.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(user);
    }

    public Task AddUserAsync(User user)
    {
        users.TryAdd(user.Id, user);

        return Task.CompletedTask;
    }

    public Task UpdateUserAsync(User user)
    {
        users.AddOrUpdate(user.Id, user, (key, value) => user);

        return Task.CompletedTask;
    }
}
