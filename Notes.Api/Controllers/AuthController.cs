using Microsoft.AspNetCore.Mvc;
using Notes.Application.DTOs.Requests;
using Notes.Application.Interfaces.ApplicationServices;

namespace Notes.Api.Controllers;

[ApiController]
[Route("[controller]/[action]/")]
public class AuthController : ControllerBase
{
    private readonly IUserAuthService _userAuthService;

    public AuthController(IUserAuthService userAuthService)
    {
        _userAuthService = userAuthService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var result = await _userAuthService.Login(loginRequestDto);

        return result.IsSuccess
            ? Ok(result.Data)
            : StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }
    
    
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequestDto signupRequestDto)
    {
        var result = await _userAuthService.Signup(signupRequestDto);

        return result.IsSuccess
            ? Ok(result.Data)
            : StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }
}