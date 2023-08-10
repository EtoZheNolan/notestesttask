using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Extensions;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.InfrastructureServices;

namespace Notes.Api.Controllers.NotesControllers;

[Route("user/notes")]
[Authorize(Roles = "User")]
public class NotesUserController : BaseNotesController
{
    public NotesUserController(INotesService notesService, ICurrentUserService currentUserService) 
        : base(notesService, currentUserService) { }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await NotesService.GetNotesByUserIdAsync(CurrentUserService.UserId!.Value);

        return result.ToActionResult();
    }
}