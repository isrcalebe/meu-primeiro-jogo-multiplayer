using Frutti.Server.Domain.DTOs;
using Frutti.Server.Domain.Entities;

namespace Frutti.Server.Domain.Services;

public interface IAuthenticationService
{
    Task<User?> RegisterUserAsync(RegisterUserDto registerUserDto);

    Task<User?> AuthenticateUserAsync(AuthenticateUserDto authenticateUserDto);
}
