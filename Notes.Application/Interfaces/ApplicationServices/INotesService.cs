using Notes.Application.DTOs.Responses;
using Notes.Application.Results;

namespace Notes.Application.Interfaces.ApplicationServices;

public interface INotesService
{
    Task<Result<List<NoteResponseDto>>> GetNotesByUsernameAsync(string username);
    
    Task<Result<List<NoteResponseDto>>> GetAllNotesAsync();
}