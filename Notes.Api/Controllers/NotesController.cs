using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.DTOs.Requests;
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
        var idInClaims = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value);
        var role = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)!.Value;

        var result = role == UserRole.Admin.ToString()
            ? await _notesService.GetAllNotesAsync()
            : await _notesService.GetNotesByAuthorIdAsync(idInClaims!);

        return result.IsSuccess ? Ok(result.Data) : StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateNoteRequestDto createNoteRequestDto)
    {
        var idInClaims = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

        if (Guid.Parse(idInClaims) != createNoteRequestDto.AuthorId)
            return Forbid();
        
        var result = await _notesService.CreateAsync(createNoteRequestDto);

        return result.IsSuccess ? Ok(result.Data) : StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }
}