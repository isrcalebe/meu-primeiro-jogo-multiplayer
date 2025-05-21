namespace Frutti.Server.Application.Requests.Responses;

public record LoginResponse(
    bool Success,
    string Message,
    string? AccessToken = null,
    string? RefreshToken = null
);
