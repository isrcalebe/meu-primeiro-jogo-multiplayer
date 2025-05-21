using System;
using System.Threading;
using System.Threading.Tasks;
using Frutti.Server.Application.Requests;
using Frutti.Server.Application.Requests.Responses;
using Frutti.Server.Domain.DTOs;
using Frutti.Server.Domain.Services;
using Mediator;

namespace Frutti.Server.Application.Handlers;

public class RegisterRequestHandler : IRequestHandler<RegisterRequest, RegisterResponse>
{
    private readonly IAuthenticationService authenticationService;

    public RegisterRequestHandler(IAuthenticationService authenticationService)
    {
        this.authenticationService = authenticationService;
    }

    public async ValueTask<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        try
        {
            await authenticationService.RegisterUserAsync(new RegisterUserDto(request.Username, request.Password)).ConfigureAwait(false);
            return new RegisterResponse(
                Success: true,
                Message: "Registration successful."
            );
        }
        catch (InvalidOperationException ex)
        {
            return new RegisterResponse(
                Success: false,
                Message: ex.Message
            );
        }
    }
}
