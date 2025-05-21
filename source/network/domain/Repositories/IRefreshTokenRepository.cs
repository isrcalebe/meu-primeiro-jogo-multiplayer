using Frutti.Server.Domain.Entities;

namespace Frutti.Server.Domain.Repositories;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> GetRefreshTokenAsync(string token);

    Task AddRefreshTokenAsync(RefreshToken token);

    Task DeleteRefreshTokenAsync(string token);

    Task DeleteAllRefreshTokensForUserAsync(Guid userId);
}
