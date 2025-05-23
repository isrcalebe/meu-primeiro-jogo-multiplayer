using System.Threading.Tasks;
using Frutti.Server.Domain.Entities;
using Frutti.Shared.Common;

namespace Frutti.Server.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task<Result<User>> RegisterUserAsync(RegisterUserDto registerUserDto);

    Task<Result<User>> AuthenticateAsync(AuthenticateUserDto authenticateUserDto);
}

public record RegisterUserDto(
    string Username,
    string Email,
    string Password
);

public record AuthenticateUserDto(
    string Username,
    string Password
);
