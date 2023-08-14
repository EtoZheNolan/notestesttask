using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Extensions;
using Notes.Application.DTOs.Requests;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.InfrastructureServices;

namespace Notes.Api.Controllers.CategoriesControllers;

[ApiController]
[Route("admin/categories")]
[Authorize(Roles = "Admin")]
public class CategoriesAdminController : BaseCategoriesController
{
    public CategoriesAdminController(ICategoriesService categoriesService, ICurrentUserService currentUserService) :
        base(categoriesService, currentUserService) { }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await CategoriesService.GetAllCategoriesAsync();

        return result.ToActionResult();
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetAllByUserId(Guid userId)
    {
        var result = await CategoriesService.GetCategoriesByUserIdAsync(userId);

        return result.ToActionResult();
    }
    
    [HttpPost("{userId:guid}")]
    public async Task<IActionResult> CreateAsync(Guid userId, CreateCategoryRequestDto requestDto)
    {
        var result = await CategoriesService.CreateAsync(userId, requestDto);

        return result.ToActionResult();
    }
}