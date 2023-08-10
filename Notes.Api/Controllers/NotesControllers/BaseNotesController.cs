using Microsoft.AspNetCore.Mvc;
using Notes.Api.Extensions;
using Notes.Application.DTOs.Requests;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.InfrastructureServices;

namespace Notes.Api.Controllers.NotesControllers;

public abstract class BaseNotesController : ControllerBase
{
    protected readonly INotesService NotesService;
    protected readonly ICurrentUserService CurrentUserService;

    protected BaseNotesController(INotesService notesService, ICurrentUserService currentUserService)
    {
        NotesService = notesService;
        CurrentUserService = currentUserService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateNoteRequestDto createNoteRequestDto)
    {
        var result = await NotesService.CreateAsync(CurrentUserService.UserId!.Value, createNoteRequestDto);

        return result.ToActionResult();
    }
}