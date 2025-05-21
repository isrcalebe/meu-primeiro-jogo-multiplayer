using Frutti.Server.Domain.Net;
using Frutti.Server.Domain.Repositories;
using Frutti.Server.Domain.Services;
using Frutti.Server.Domain.Utilities;
using Frutti.Server.Infrastructure.Data;
using Frutti.Server.Infrastructure.Net;
using Frutti.Server.Infrastructure.Security;
using Frutti.Server.Infrastructure.Services;
using Frutti.Server.Infrastructure.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Frutti.Server.Infrastructure.Extensions.ServiceCollectionExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddSingleton<ITokenService>(provider =>
            new JwtTokenService(configuration["Security:JwtSecret"], configuration["Security:JwtIssuer"], configuration["Security:JwtAudience"]));

        services.AddSingleton<IAuthenticationService, AuthenticationService>();

        services.AddSingleton<IPacketHandler, PacketHandler>();

        return services;
    }
}
