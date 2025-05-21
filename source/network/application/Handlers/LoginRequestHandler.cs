using System;
using System.Threading;
using System.Threading.Tasks;
using Frutti.Server.Application.Requests;
using Frutti.Server.Application.Requests.Responses;
using Frutti.Server.Domain.DTOs;
using Frutti.Server.Domain.Entities;
using Frutti.Server.Domain.Repositories;
using Frutti.Server.Domain.Services;
using Mediator;

namespace Frutti.Server.Application.Handlers;

public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    private readonly IAuthenticationService authenticationService;
    private readonly ITokenService tokenService;
    private readonly IUserRepository userRepository;

    public LoginRequestHandler(IAuthenticationService authenticationService, ITokenService tokenService, IUserRepository userRepository)
    {
        this.authenticationService = authenticationService;
        this.tokenService = tokenService;
        this.userRepository = userRepository;
    }

    public async ValueTask<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await authenticationService.AuthenticateUserAsync(new AuthenticateUserDto(request.Username, request.Password)).ConfigureAwait(false);

        if (user == null)
        {
            return new LoginResponse(
                Success: false,
                Message: "Invalid credentials."
            );
        }

        var accessToken = tokenService.GenerateAccessToken(user);
        var refreshTokenString = tokenService.GenerateRefreshToken();
        var refreshToken = new RefreshToken(refreshTokenString, DateTime.UtcNow.AddHours(1), user.Id);

        user.RefreshTokens.RemoveAll(token => token.IsExpired);
        user.AddRefreshToken(refreshToken);

        await userRepository.UpdateUserAsync(user).ConfigureAwait(false);

        return new LoginResponse(
            Success: true,
            Message: "Login successful.",
            AccessToken: accessToken,
            RefreshToken: refreshTokenString
        );
    }
}
