using Notes.Application.DTOs.Requests;
using Notes.Application.DTOs.Responses;
using Notes.Application.Results;

namespace Notes.Application.Interfaces.ApplicationServices;

public interface IUserAuthService
{
    Task<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDto);
    
    Task<Result<SignupResponseDto>> Signup(SignupRequestDto signupRequestDto);
}