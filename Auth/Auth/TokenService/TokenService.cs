using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Auth.TokenService;

public class TokenService : ITokenService
{
    private readonly TimeSpan _tokenDuration = new(0, 60, 0);
    
    public string BuildToken(string key, string issuer, UserEntity user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Nickname),
            new Claim(ClaimTypes.Email, user.Login),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        var tokenDescriptor = new JwtSecurityToken(issuer, issuer, claims, 
            expires: DateTime.Now.Add(_tokenDuration), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
    }
}