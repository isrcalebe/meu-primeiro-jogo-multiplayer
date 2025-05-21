namespace Frutti.Server.Domain.Utilities;

public interface IPasswordHasher
{
    string HashPassword(string password);

    bool VerifyHashedPassword(string password, string hashedPassword);
}
