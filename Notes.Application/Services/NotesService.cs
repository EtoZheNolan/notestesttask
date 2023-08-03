using AutoMapper;
using Notes.Application.DTOs.Requests;
using Notes.Application.DTOs.Responses;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.Repositories;
using Notes.Application.Results;
using Notes.Domain.Entities;

namespace Notes.Application.Services;

public class NotesService : INotesService
{
    private readonly INotesRepository _notesRepository;
    private readonly IMapper _mapper;

    public NotesService(INotesRepository notesRepository, IMapper mapper)
    {
        _notesRepository = notesRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<NoteResponseDto>>> GetNotesByUsernameAsync(string username)
    {
        var result = await _notesRepository.GetNotesByUsernameAsync(username, true);

        return Result<List<NoteResponseDto>>.Success(_mapper.Map<List<Note>, List<NoteResponseDto>>(result));
    }

    public async Task<Result<List<NoteResponseDto>>> GetAllNotesAsync()
    {
        var result = await _notesRepository.GetAllAsync(true);

        return Result<List<NoteResponseDto>>.Success(_mapper.Map<List<Note>, List<NoteResponseDto>>(result));
    }

    public async Task<Result<bool>> CreateAsync(CreateNoteRequestDto createNoteRequestDto)
    {
        var entity = _mapper.Map<CreateNoteRequestDto, Note>(createNoteRequestDto);

        await _notesRepository.AddAsync(entity);
        await _notesRepository.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}