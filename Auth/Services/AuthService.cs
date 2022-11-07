using Auth.Auth.TokenService;
using Auth.Domain.Entities;
using Auth.Domain.Models;
using Auth.UserDbContext.Repositories.UserRepository;

namespace Auth.Services;

public sealed class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthService(IUserRepository userRepository, ITokenService tokenService, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task<UserEntity> Register(UserModel user)
    {
        var userEntity = new UserEntity()
        {
            Nickname = user.Nickname,
            Login = user.Email,
            Password = user.Password
        };
        var userResult = await _userRepository.Add(userEntity);

        return userResult;
    }

    public async Task<string> Login(LoginModel loginModel)
    {
        var userModel = new UserModel()
        {
            Email = loginModel.Email,
            Password = loginModel.Password
        };

        var user = await _userRepository.GetUser(userModel);
        var token = _tokenService?.BuildToken(_configuration?["Jwt:Key"], _configuration?["Jwt:Issuer"], user);

        return token;
    }
}