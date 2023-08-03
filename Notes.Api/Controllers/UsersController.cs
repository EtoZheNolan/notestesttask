using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.DTOs.Requests;
using Notes.Application.Interfaces.ApplicationServices;

namespace Notes.Api.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("[controller]/[action]/")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _usersService.GetAllUsers();

        return result.IsSuccess ? Ok(result.Data) : StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }

    [HttpPost("update-user-role")]
    public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleRequestDto updateUserRoleRequestDto)
    {
        var result = await _usersService.UpdateUserRoleAsync(updateUserRoleRequestDto);
        
        return result.IsSuccess ? Ok(result.Data) : StatusCode((int)result.HttpStatusCode, result.ErrorMessage); 
    }
}