using Microsoft.Extensions.Caching.Memory;
using Notes.Application.DTOs.Responses;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Results;

namespace Notes.Infrastructure.Decorators;

public class CachingNotesDecorator : INotesService
{
    private readonly INotesService _notesService;
    private readonly IMemoryCache _memoryCache;

    public CachingNotesDecorator(INotesService notesService, IMemoryCache memoryCache)
    {
        _notesService = notesService;
        _memoryCache = memoryCache;
    }
    
    public async Task<Result<List<NoteResponseDto>>> GetNotesByUsernameAsync(string username)
    {
        var cacheKey = $"UserNotes_{username}";

        if (_memoryCache.TryGetValue(cacheKey, out List<NoteResponseDto>? notes))
            return Result<List<NoteResponseDto>>.Success(notes!);

        var result = await _notesService.GetNotesByUsernameAsync(username);

        if (result.Data is null || !result.Data.Any())
            return result;

        _memoryCache.Set(cacheKey, result.Data, TimeSpan.FromMinutes(10));
        return result;
    }

    public async Task<Result<List<NoteResponseDto>>> GetAllNotesAsync()
    {
        return await _notesService.GetAllNotesAsync();
    }
}