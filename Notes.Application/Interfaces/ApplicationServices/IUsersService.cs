using Notes.Application.DTOs.Responses;
using Notes.Application.Results;

namespace Notes.Application.Interfaces.ApplicationServices;

public interface IUsersService
{
    public Task<Result<List<UserResponseDto>>> GetAllUsers();
}