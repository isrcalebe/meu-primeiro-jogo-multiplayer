namespace Frutti.Server.Domain.Entities;

public class User
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Username { get; private set; }

    public string PasswordHash { get; private set; }

    public List<RefreshToken> RefreshTokens { get; private set; } = [];

    public User(string username, string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(username);
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);

        Username = username;
        PasswordHash = passwordHash;
    }

    public void AddRefreshToken(RefreshToken token)
        => RefreshTokens.Add(token);

    public void RemoveRefreshToken(RefreshToken token)
        => RefreshTokens.Remove(token);

    public RefreshToken? GetRefreshToken(string token)
        => RefreshTokens.FirstOrDefault(refresh => refresh.Token == token);
}
