using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Application.DTOs.Requests;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.InfrastructureServices;
using Notes.Domain.Enums;

namespace Notes.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]/")]
public class NotesController : ControllerBase
{
    private readonly INotesService _notesService;
    private readonly ICurrentUserService _currentUserService;

    public NotesController(INotesService notesService, ICurrentUserService currentUserService)
    {
        _notesService = notesService;
        _currentUserService = currentUserService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = _currentUserService.UserRole == UserRole.Admin
            ? await _notesService.GetAllNotesAsync()
            : await _notesService.GetNotesByAuthorIdAsync(_currentUserService.UserId!.Value);

        return result.IsSuccess ? Ok(result.Data) : StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateNoteRequestDto createNoteRequestDto)
    {
        if (_currentUserService.UserId != createNoteRequestDto.AuthorId)
            return Forbid();
        
        var result = await _notesService.CreateAsync(createNoteRequestDto);

        return result.IsSuccess ? Ok(result.Data) : StatusCode((int)result.HttpStatusCode, result.ErrorMessage);
    }
}