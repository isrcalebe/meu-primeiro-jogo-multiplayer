using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Frutti.Server.Domain.Entities;
using Frutti.Server.Domain.Services;
using Microsoft.IdentityModel.Tokens;

namespace Frutti.Server.Infrastructure.Security;

public class JwtTokenService : ITokenService
{
    private readonly string jwtSecret;
    private readonly string issuer;
    private readonly string audience;

    public JwtTokenService(string jwtSecret, string issuer, string audience)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(jwtSecret);
        ArgumentException.ThrowIfNullOrWhiteSpace(issuer);
        ArgumentException.ThrowIfNullOrWhiteSpace(audience);

        this.jwtSecret = jwtSecret;
        this.issuer = issuer;
        this.audience = audience;
    }

    public string GenerateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(jwtSecret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using var rng = RandomNumberGenerator.Create();

        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
