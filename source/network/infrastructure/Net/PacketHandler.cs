using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Frutti.Server.Application.Handlers;
using Frutti.Server.Application.Requests;
using Frutti.Server.Domain.Net;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Frutti.Server.Infrastructure.Net;

public class PacketHandler : IPacketHandler
{
    private readonly IServiceProvider serviceProvider;
    private readonly ILogger<PacketHandler> logger;
    private readonly JsonSerializerOptions jsonSerializerOptions;
    private readonly CancellationTokenSource cancellationTokenSource;

    public PacketHandler(IServiceProvider serviceProvider, ILogger<PacketHandler> logger)
    {
        this.serviceProvider = serviceProvider;
        this.logger = logger;
        jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        cancellationTokenSource = new CancellationTokenSource();
    }

    public async Task<string> HandlePacketAsync(string rawPacket)
    {
        try
        {
            var packetEnvelope = JsonSerializer.Deserialize<PacketEnvelope>(rawPacket, jsonSerializerOptions);

            if (packetEnvelope == null || string.IsNullOrEmpty(packetEnvelope.Type))
            {
                return JsonSerializer.Serialize(new
                {
                    Success = false,
                    Message = "Invalid packet format."
                });
            }

            logger.LogInformation("Handling packet type: {PacketType}", packetEnvelope.Type);

            using var scope = serviceProvider.CreateScope();

            switch (packetEnvelope.Type)
            {
                case "Register":
                    var registerRequest = JsonSerializer.Deserialize<RegisterRequest>(packetEnvelope.Data.ToString(), jsonSerializerOptions);
                    var registerHandler = scope.ServiceProvider.GetRequiredService<RegisterRequestHandler>();
                    var registerResult = await registerHandler.Handle(registerRequest, cancellationTokenSource.Token);
                    return JsonSerializer.Serialize(registerResult);

                case "Login":
                    var loginRequest = JsonSerializer.Deserialize<LoginRequest>(packetEnvelope.Data.ToString(), jsonSerializerOptions);
                    var loginHandler = scope.ServiceProvider.GetRequiredService<LoginRequestHandler>();
                    var loginResult = await loginHandler.Handle(loginRequest, cancellationTokenSource.Token);
                    return JsonSerializer.Serialize(loginResult);

                case "RefreshToken":
                    var refreshTokenRequest = JsonSerializer.Deserialize<RefreshTokenRequest>(packetEnvelope.Data.ToString(), jsonSerializerOptions);
                    var refreshTokenHandler = scope.ServiceProvider.GetRequiredService<RefreshTokenRequestHandler>();
                    var refreshTokenResult = await refreshTokenHandler.Handle(refreshTokenRequest, cancellationTokenSource.Token);
                    return JsonSerializer.Serialize(refreshTokenResult);

                default:
                    return JsonSerializer.Serialize(new { Success = false, Message = $"Unknown packet type: {packetEnvelope.Type}" });
            }
        }
        catch (JsonException jsonEx)
        {
            logger.LogError(jsonEx, "JSON deserialization error: {Message} - Raw packet: {RawPacket}", jsonEx.Message, rawPacket);
            return JsonSerializer.Serialize(new { Success = false, Message = "Invalid JSON data." });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error handling packet: {Message} - Raw packet: {RawPacket}", ex.Message, rawPacket);
            return JsonSerializer.Serialize(new { Success = false, Message = "Internal server error." });
        }
    }
}
