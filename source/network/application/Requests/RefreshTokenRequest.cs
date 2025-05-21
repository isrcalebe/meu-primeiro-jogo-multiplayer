using Frutti.Server.Application.Requests.Responses;
using Mediator;

namespace Frutti.Server.Application.Requests;

public record RefreshTokenRequest(
    string ExpiredAccessToken,
    string RefreshToken
) : IRequest<RefreshTokenResponse>;
