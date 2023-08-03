using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.DTOs.Requests;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Domain.Enums;

namespace Notes.Api.Controllers;

[ApiController]
[Route("[controller]/[action]/")]
[Authorize]
public class CategoriesController : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesController(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var username = HttpContext.User.Identity!.Name;
        var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)!.Value;

        var result = role == UserRole.Admin.ToString()
            ? await _categoriesService.GetAllCategoriesAsync()
            : await _categoriesService.GetCategoriesByUsernameAsync(username!);

        return result.IsSuccess ? Ok(result.Data) : StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryRequestDto createNoteRequestDto)
    {
        var result = await _categoriesService.CreateAsync(createNoteRequestDto);

        return result.IsSuccess ? Ok(result.Data) : StatusCode((int)result.HttpStatusCode, result.Data);
    }
}