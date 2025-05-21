using System;
using System.Threading.Tasks;
using Frutti.Server.Domain.DTOs;
using Frutti.Server.Domain.Entities;
using Frutti.Server.Domain.Repositories;
using Frutti.Server.Domain.Services;
using Frutti.Server.Domain.Utilities;

namespace Frutti.Server.Infrastructure.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository userRepository;
    private readonly IPasswordHasher passwordHasher;

    public AuthenticationService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        this.userRepository = userRepository;
        this.passwordHasher = passwordHasher;
    }

    public async Task<User?> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        var existingUser = await userRepository.GetUserByUsernameAsync(registerUserDto.Username).ConfigureAwait(false);

        if (existingUser != null)
            throw new InvalidOperationException("Username already exists.");

        var passwordHash = passwordHasher.HashPassword(registerUserDto.Password);

        var newUser = new User(registerUserDto.Username, passwordHash);
        await userRepository.AddUserAsync(newUser).ConfigureAwait(false);

        return newUser;
    }

    public async Task<User?> AuthenticateUserAsync(AuthenticateUserDto authenticateUserDto)
    {
        var user = await userRepository.GetUserByUsernameAsync(authenticateUserDto.Username).ConfigureAwait(false);

        if (user == null)
            throw new InvalidOperationException("User does not exists!");

        if (!passwordHasher.VerifyHashedPassword(authenticateUserDto.Password, user.PasswordHash))
            throw new InvalidOperationException("Invalid password!");

        return user;
    }
}
