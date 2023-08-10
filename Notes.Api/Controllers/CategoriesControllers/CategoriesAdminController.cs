using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Notes.Api.Controllers.CategoriesControllers;

[ApiController]
[Route("admin/categories")]
[Authorize]
public class CategoriesAdminController : BaseCategoriesController
{
    
}