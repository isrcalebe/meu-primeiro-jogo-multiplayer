using Frutti.Server.Application.Security;
using Frutti.Server.Domain.Providers;
using Frutti.Server.Domain.Repositories;
using Frutti.Server.Infrastructure.Options;
using Frutti.Server.Infrastructure.Providers;
using Frutti.Server.Infrastructure.Repositories.InMemory;
using Frutti.Server.Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Frutti.Server.Infrastructure.Extensions.ServiceCollectionExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();
        services.AddTransient<IPasswordHasher, BCryptPasswordHasher>();

        return services;
    }

    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ServerOptions>(configuration.GetSection("Server"));

        return services;
    }
}
