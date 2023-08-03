using System.Net;
using AutoMapper;
using Notes.Application.DTOs.Requests;
using Notes.Application.DTOs.Responses;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.Repositories;
using Notes.Application.Results;
using Notes.Domain.Entities;

namespace Notes.Application.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public UsersService(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<List<UserResponseDto>>> GetAllUsers()
    {
        var result = await _usersRepository.GetAllAsync();

        return Result<List<UserResponseDto>>.Success(_mapper.Map<List<User>, List<UserResponseDto>>(result));
    }

    public async Task<Result<bool>> UpdateUserRoleAsync(UpdateUserRoleRequestDto updateUserRoleRequestDto)
    {
        var user = await _usersRepository.GetByIdAsync(updateUserRoleRequestDto.UserId);

        if (user is null)
            return Result<bool>.Failure(HttpStatusCode.NotFound, "User not found");

        user.Role = updateUserRoleRequestDto.UserRole;
        await _usersRepository.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}