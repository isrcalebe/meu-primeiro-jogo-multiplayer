namespace Frutti.Server.Application.Security;

public interface IPasswordHasher
{
    string HashPassword(string password);

    bool VerifyHashedPassword(string password, string hashedPassword);
}
