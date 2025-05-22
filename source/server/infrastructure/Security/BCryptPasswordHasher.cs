using Frutti.Server.Application.Security;

namespace Frutti.Server.Infrastructure.Security;

public class BCryptPasswordHasher : IPasswordHasher
{
    private const int work_factor = 12;

    public string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password, work_factor);

    public bool VerifyHashedPassword(string password, string hashedPassword)
        => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}
