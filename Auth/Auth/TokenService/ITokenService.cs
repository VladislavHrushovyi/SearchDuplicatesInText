using Auth.Domain.Entities;
using Auth.Domain.Models;

namespace Auth.Auth.TokenService;

public interface ITokenService
{
    public string BuildToken(string key, string issuer, UserEntity user);
}