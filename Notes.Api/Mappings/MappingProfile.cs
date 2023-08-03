using AutoMapper;
using Notes.Application.DTOs.Requests;
using Notes.Application.DTOs.Responses;
using Notes.Domain.Entities;

namespace Notes.Api.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Note, NoteResponseDto>();
        
        
        CreateMap<SignupRequestDto, User>();
    }
}