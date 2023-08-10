using Notes.Application.DTOs.Requests;
using Notes.Application.DTOs.Responses;
using Notes.Application.Results;

namespace Notes.Application.Interfaces.ApplicationServices;

public interface INotesService
{
    Task<Result<List<NoteResponseDto>>> GetNotesByUserIdAsync(Guid userId);
    
    Task<Result<List<NoteResponseDto>>> GetAllNotesAsync();

    Task<Result<CreateNoteResponseDto>> CreateAsync(Guid userId, CreateNoteRequestDto createNoteRequestDto);
}