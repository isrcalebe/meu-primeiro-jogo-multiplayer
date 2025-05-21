using Frutti.Server.Domain.Entities;

namespace Frutti.Server.Domain.Services;

public interface ITokenService
{
    string GenerateAccessToken(User user);

    string GenerateRefreshToken();
}
