namespace Frutti.Server.Application.Requests.Responses;

public record RefreshTokenResponse(
    bool Success,
    string Message,
    string? AccessToken = null,
    string? NewRefreshToken = null
);
