using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notes.Api.Extensions;
using Notes.Application.DTOs.Requests;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.InfrastructureServices;

namespace Notes.Api.Controllers.NotesControllers;

[Route("admin/notes")]
[Authorize(Roles = "Admin")]
public class NotesAdminController : BaseNotesController
{
    public NotesAdminController(INotesService notesService, ICurrentUserService currentUserService) 
        : base(notesService, currentUserService) { }
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await NotesService.GetAllNotesAsync();

        return result.ToActionResult();
    }
    
    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetAllByUserId(Guid userId)
    {
        var result = await NotesService.GetNotesByUserIdAsync(userId);

        return result.ToActionResult();
    }
    
    [HttpPost("{userId:guid}")]
    public async Task<IActionResult> CreateAsync(Guid userId, CreateNoteRequestDto requestDto)
    {
        var result = await NotesService.CreateAsync(userId, requestDto);

        return result.ToActionResult();
    }
}