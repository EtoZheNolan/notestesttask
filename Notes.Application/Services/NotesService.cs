using System.Net;
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
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public NotesService(INotesRepository notesRepository, IUsersRepository usersRepository, IMapper mapper)
    {
        _notesRepository = notesRepository;
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<NoteResponseDto>>> GetNotesByAuthorIdAsync(Guid userId)
    {
        var result = await _notesRepository.GetNotesByUserIdAsync(userId, true);

        return Result<List<NoteResponseDto>>.Success(_mapper.Map<List<Note>, List<NoteResponseDto>>(result));
    }

    public async Task<Result<List<NoteResponseDto>>> GetAllNotesAsync()
    {
        var result = await _notesRepository.GetAllAsync(true);

        return Result<List<NoteResponseDto>>.Success(_mapper.Map<List<Note>, List<NoteResponseDto>>(result));
    }

    public async Task<Result<CreateNoteResponseDto>> CreateAsync(CreateNoteRequestDto createNoteRequestDto)
    {
        var user = await _usersRepository.GetByIdAsync(createNoteRequestDto.AuthorId);

        if (user is null)
            return Result<CreateNoteResponseDto>.Failure(HttpStatusCode.NotFound, "User doesn't exist");
        
        var entity = _mapper.Map<CreateNoteRequestDto, Note>(createNoteRequestDto);

        await _notesRepository.AddAsync(entity);
        await _notesRepository.SaveChangesAsync();

        return Result<CreateNoteResponseDto>.Success(new CreateNoteResponseDto { Id = entity.Id });
    }
}