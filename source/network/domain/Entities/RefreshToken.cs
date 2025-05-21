namespace Frutti.Server.Domain.Entities;

public class RefreshToken
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid UserId { get; private set; }

    public string Token { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public RefreshToken(string token, DateTime expiresAt, Guid userId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(token);

        Token = token;
        CreatedAt = DateTime.UtcNow;
        ExpiresAt = expiresAt;

        UserId = userId;
    }

    private RefreshToken()
    {
    }
}
