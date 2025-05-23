using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Frutti.Server.Domain.Entities;
using Frutti.Server.Domain.Repositories;

namespace Frutti.Server.Infrastructure.Repositories.InMemory;

public class InMemoryUserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<Guid, User> users = [];

    public Task<User?> GetUserByIdAsync(Guid id)
    {
        users.TryGetValue(id, out var user);

        return Task.FromResult(user);
    }

    public Task<User?> GetUserByUsername(string username)
    {
        var user = users.Values.FirstOrDefault(match => match.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

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

    public Task<bool> DeleteUserAsync(Guid id)
    {
        var result = users.TryRemove(id, out _);

        return Task.FromResult(result);
    }
}
