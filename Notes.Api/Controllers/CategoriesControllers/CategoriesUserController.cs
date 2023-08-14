using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Extensions;
using Notes.Application.DTOs.Requests;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.InfrastructureServices;

namespace Notes.Api.Controllers.CategoriesControllers;

[ApiController]
[Route("user/categories")]
[Authorize(Roles = "User")]
public class CategoriesUserController : BaseCategoriesController
{
    public CategoriesUserController(ICategoriesService categoriesService, ICurrentUserService currentUserService) :
        base(categoriesService, currentUserService) { }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await CategoriesService.GetCategoriesByUserIdAsync(CurrentUserService.UserId!.Value);

        return result.ToActionResult();
    }
}