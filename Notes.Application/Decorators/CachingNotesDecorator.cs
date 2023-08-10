using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Notes.Application.DTOs.Requests;
using Notes.Application.DTOs.Responses;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Results;
using Notes.Application.Settings;

namespace Notes.Application.Decorators;

public class CachingNotesDecorator : INotesService
{
    private readonly INotesService _notesService;
    private readonly ICacheService _cacheService;
    private readonly CacheSettings _cacheSettings;

    public CachingNotesDecorator(INotesService notesService, ICacheService cacheService, IOptions<CacheSettings> cacheOptions)
    {
        _notesService = notesService;
        _cacheService = cacheService;
        _cacheSettings = cacheOptions.Value;
    }
    
    public async Task<Result<List<NoteResponseDto>>> GetNotesByUserIdAsync(Guid userId)
    {
        var cacheKey = $"UserNotes:{userId}";

        if (_cacheService.TryGetValue(cacheKey, out List<NoteResponseDto>? notes))
            return Result<List<NoteResponseDto>>.Success(notes!);

        var result = await _notesService.GetNotesByUserIdAsync(userId);

        if (result.Data is null || !result.Data.Any())
            return result;

        _cacheService.Set(cacheKey, result.Data, TimeSpan.FromMinutes(_cacheSettings.ExpirationTimeInMinutes));
        return result;
    }

    public async Task<Result<List<NoteResponseDto>>> GetAllNotesAsync()
    {
        return await _notesService.GetAllNotesAsync();
    }

    public async Task<Result<CreateNoteResponseDto>> CreateAsync(Guid userId, CreateNoteRequestDto createNoteRequestDto)
    {
        _cacheService.Remove($"UserNotes:{userId}");
        
        return await _notesService.CreateAsync(userId, createNoteRequestDto);
    }
}