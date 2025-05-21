using Frutti.Server.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Frutti.Server.Application.Extensions.ServiceCollectionExtensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<LoginRequestHandler>();
        services.AddScoped<RegisterRequestHandler>();
        services.AddScoped<RefreshTokenRequestHandler>();

        return services;
    }
}
