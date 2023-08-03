using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Domain.Enums;

namespace Notes.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]/")]
public class NotesController : ControllerBase
{
    private readonly INotesService _notesService;

    public NotesController(INotesService notesService)
    {
        _notesService = notesService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var username = HttpContext.User.Identity!.Name;
        var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)!.Value;

        var result = role == UserRole.Admin.ToString()
            ? await _notesService.GetAllNotesAsync()
            : await _notesService.GetNotesByUsernameAsync(username!);

        return result.IsSuccess ? Ok(result.Data) : StatusCode((int)result.HttpStatusCode, result.Data);
    }
}