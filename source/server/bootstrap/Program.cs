using System.IO;
using Frutti.Server.Infrastructure.Extensions.ServiceCollectionExtensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory());
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

Log.Logger = new LoggerConfiguration()
             .WriteTo.Console(
                 outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:w}]: {Message:lj}{NewLine}{Exception}"
             )
             .Enrich.FromLogContext()
             .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger, true);

builder.Services.AddInfrastructure();
builder.Services.ConfigureInfrastructure(builder.Configuration);

var host = builder.Build();

await host.RunAsync().ConfigureAwait(false);
