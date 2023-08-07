using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.DTOs.Requests;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.InfrastructureServices;
using Notes.Domain.Enums;

namespace Notes.Api.Controllers;

[ApiController]
[Route("[controller]/[action]/")]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;
    private readonly ICurrentUserService _currentUserService;

    public CategoriesController(ICategoriesService categoriesService, ICurrentUserService currentUserService)
    {
        _categoriesService = categoriesService;
        _currentUserService = currentUserService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = _currentUserService.UserRole == UserRole.Admin
            ? await _categoriesService.GetAllCategoriesAsync()
            : await _categoriesService.GetCategoriesByUsernameAsync(_currentUserService.Username!);

        return result.IsSuccess ? Ok(result.Data) : StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequestDto createNoteRequestDto)
    {
        if (_currentUserService.UserId != createNoteRequestDto.AuthorId)
            return Forbid();
        
        var result = await _categoriesService.CreateAsync(createNoteRequestDto);

        return result.IsSuccess ? Ok(result.Data) : StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }
}