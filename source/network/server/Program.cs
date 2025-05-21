using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Frutti.Server.Application.Extensions.ServiceCollectionExtensions;
using Frutti.Server.Domain.Net;
using Frutti.Server.Infrastructure.Extensions.ServiceCollectionExtensions;
using Frutti.Server.Net;
using Frutti.Server.Services;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", false, true);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true);
builder.Configuration.AddEnvironmentVariables(source =>
{
    source.Prefix = "FRUTTI_";
});

Log.Logger = new LoggerConfiguration()
             .WriteTo.Console(
                 outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:w}]: {Message:lj}{NewLine}{Exception}"
             )
             .Enrich.FromLogContext()
             .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddSingleton<FruttiServerService>(provider =>
{
    var logger = provider.GetRequiredService<ILogger<FruttiServerService>>();
    var packetHandler = provider.GetRequiredService<IPacketHandler>();

    return new FruttiServerService(logger, builder.Configuration.GetValue<int>("Net:Port", 6666), clientHandlerFactory);

    async Task clientHandlerFactory(TcpClient client)
    {
        var connectionLogger = provider.GetRequiredService<ILogger<FruttiConnectionHandler>>();
        var handler = new FruttiConnectionHandler(client, connectionLogger, packetHandler.HandlePacketAsync);
        await handler.HandleConnectionAsync(CancellationToken.None).ConfigureAwait(false);
    }
});

builder.Services.AddHostedService(provider => provider.GetRequiredService<FruttiServerService>());

var server = builder.Build();
await server.RunAsync().ConfigureAwait(false);
