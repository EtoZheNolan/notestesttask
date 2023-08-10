using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Extensions;
using Notes.Application.DTOs.Requests;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.InfrastructureServices;
using Notes.Domain.Enums;

namespace Notes.Api.Controllers.CategoriesControllers;

[ApiController]
[Route("user/categories")]
[Authorize]
public class CategoriesUserController : BaseCategoriesController
{
    private readonly ICategoriesService _categoriesService;
    private readonly ICurrentUserService _currentUserService;

    public CategoriesUserController(ICategoriesService categoriesService, ICurrentUserService currentUserService)
    {
        _categoriesService = categoriesService;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _categoriesService.GetCategoriesByUserIdAsync(_currentUserService.UserId!.Value);

        return result.ToActionResult();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequestDto createNoteRequestDto)
    {
        var result = await _categoriesService.CreateAsync(_currentUserService.UserId!.Value, createNoteRequestDto);

        return result.ToActionResult();
    }
}