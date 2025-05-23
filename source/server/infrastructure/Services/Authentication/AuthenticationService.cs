using System;
using System.Threading.Tasks;
using Frutti.Server.Application.Security;
using Frutti.Server.Application.Services.Authentication;
using Frutti.Server.Domain.Entities;
using Frutti.Server.Domain.Providers;
using Frutti.Server.Domain.Repositories;
using Frutti.Shared.Common;
using Microsoft.Extensions.Logging;

namespace Frutti.Server.Infrastructure.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILogger<AuthenticationService> logger;

    private readonly IUserRepository userRepository;
    private readonly IPasswordHasher passwordHasher;
    private readonly IDateTimeProvider dateTimeProvider;

    public AuthenticationService(ILogger<AuthenticationService> logger, IUserRepository userRepository, IPasswordHasher passwordHasher, IDateTimeProvider dateTimeProvider)
    {
        this.logger = logger;
        this.userRepository = userRepository;
        this.passwordHasher = passwordHasher;
        this.dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<User>> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        var existingUser = await userRepository.GetUserByUsername(registerUserDto.Username).ConfigureAwait(false);

        if (existingUser is not null)
            return ResultExtensions.Error<User>(Errors.User.USERNAME_ALREADY_EXISTS);

        var passwordHash = passwordHasher.HashPassword(registerUserDto.Password);
        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Username = registerUserDto.Username,
            Email = registerUserDto.Email,
            PasswordHash = passwordHash,
            CreatedAt = dateTimeProvider.UtcNow,
            UpdatedAt = dateTimeProvider.UtcNow,
        };

        await userRepository.AddUserAsync(newUser).ConfigureAwait(false);

        return ResultExtensions.Ok(newUser);
    }

    public async Task<Result<User>> AuthenticateAsync(AuthenticateUserDto authenticateUserDto)
    {
        var user = await userRepository.GetUserByUsername(authenticateUserDto.Username).ConfigureAwait(false);

        if (user is null)
            return ResultExtensions.Error<User>(Errors.User.USER_NOT_FOUND);

        if (!passwordHasher.VerifyHashedPassword(authenticateUserDto.Password, user.PasswordHash))
            return ResultExtensions.Error<User>(Errors.User.INVALID_CREDENTIALS);

        return ResultExtensions.Ok(user);
    }
}
