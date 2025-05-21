namespace Frutti.Server.Domain.DTOs;

public record AuthenticateUserDto(
    string Username,
    string Password
);
