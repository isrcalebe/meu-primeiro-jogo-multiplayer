using Frutti.Server.Application.Security;
using Frutti.Server.Domain.Repositories;
using Frutti.Server.Infrastructure.Repositories.InMemory;
using Frutti.Server.Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;

namespace Frutti.Server.Infrastructure.Extensions.ServiceCollectionExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this ServiceCollection services)
    {
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();

        services.AddTransient<IPasswordHasher, BCryptPasswordHasher>();

        return services;
    }
}
