using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frutti.Server.Application.Requests;
using Frutti.Server.Application.Requests.Responses;
using Frutti.Server.Domain.Entities;
using Frutti.Server.Domain.Repositories;
using Frutti.Server.Domain.Services;
using Mediator;

namespace Frutti.Server.Application.Handlers;

public class RefreshTokenRequestHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
{
    private readonly ITokenService tokenService;
    private readonly IUserRepository userRepository;

    public RefreshTokenRequestHandler(ITokenService tokenService, IUserRepository userRepository)
    {
        this.tokenService = tokenService;
        this.userRepository = userRepository;
    }

    public async ValueTask<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(request.ExpiredAccessToken) as JwtSecurityToken;

        if (jsonToken == null)
        {
            return new RefreshTokenResponse(
                Success: false,
                Message: "Invalid access token."
            );
        }

        var userIdClaim = jsonToken.Claims.FirstOrDefault(c => c.Type == "id");

        if (userIdClaim == null || !Guid.TryParse((string?)userIdClaim.Value, out Guid userId))
        {
            return new RefreshTokenResponse(
                Success: false,
                Message: "Invalid user ID in access token."
            );
        }

        var user = await userRepository.GetUserByIdAsync(userId).ConfigureAwait(false);

        if (user == null)
        {
            return new RefreshTokenResponse(
                Success: false,
                Message: "User not found."
            );
        }

        var storedRefreshToken = user.GetRefreshToken(request.RefreshToken);

        if (storedRefreshToken == null || storedRefreshToken.IsExpired)
        {
            user.RefreshTokens.Clear();
            await userRepository.UpdateUserAsync(user).ConfigureAwait(false);
            return new RefreshTokenResponse(
                Success: false,
                Message: "Invalid or expired refresh token. Please log in again."
            );
        }

        var newAccessToken = tokenService.GenerateAccessToken(user);
        var newRefreshTokenString = tokenService.GenerateRefreshToken();
        var newRefreshToken = new RefreshToken(newRefreshTokenString, DateTime.UtcNow.AddHours(1), user.Id);

        user.RemoveRefreshToken(storedRefreshToken);
        user.AddRefreshToken(newRefreshToken);
        await userRepository.UpdateUserAsync(user).ConfigureAwait(false);

        return new RefreshTokenResponse
        (
            Success: true,
            AccessToken: newAccessToken,
            NewRefreshToken: newRefreshTokenString,
            Message: "Tokens refreshed successfully."
        );
    }
}
