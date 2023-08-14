using Microsoft.AspNetCore.Mvc;
using Notes.Api.Extensions;
using Notes.Application.DTOs.Requests;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.InfrastructureServices;

namespace Notes.Api.Controllers.CategoriesControllers;

public abstract class BaseCategoriesController : ControllerBase
{
    protected readonly ICategoriesService CategoriesService;
    protected readonly ICurrentUserService CurrentUserService;

    protected BaseCategoriesController(ICategoriesService categoriesService, ICurrentUserService currentUserService)
    {
        CategoriesService = categoriesService;
        CurrentUserService = currentUserService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryRequestDto createNoteRequestDto)
    {
        var result = await CategoriesService.CreateAsync(CurrentUserService.UserId!.Value, createNoteRequestDto);

        return result.ToActionResult();
    }
}