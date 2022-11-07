using Auth.Domain.Models;
using Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Auth.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IResult> Register([FromBody] UserModel user)
    {
        var result = await _authService.Register(user);

        return Results.Ok(result);
    }

    [AllowAnonymous]
    [HttpGet("login")]
    public async Task<IResult> Login([FromQuery] LoginModel model)
    {
        var result = await _authService.Login(model);
        
        return Results.Ok(result);
    }

    [Authorize]
    [HttpGet("jwt-isValid")]
    public async Task<IResult> JwtIsValid()
    {
        var id = this.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
        var email = this.User.Claims.First(i => i.Type == ClaimTypes.Email).Value;
        var nickname = this.User.Claims.First(i => i.Type == ClaimTypes.Name).Value;

        return Results.Ok(new
        {
            Id = id,
            Email = email,
            Nickname = nickname
        });
    }
}