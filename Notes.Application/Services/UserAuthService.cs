using System.Net;
using AutoMapper;
using Notes.Application.DTOs.Requests;
using Notes.Application.DTOs.Responses;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.InfrastructureServices;
using Notes.Application.Interfaces.Repositories;
using Notes.Application.Results;
using Notes.Domain.Entities;

namespace Notes.Application.Services;

public class UserAuthService : IUserAuthService
{
    private readonly IUsersRepository _usersRepository;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public UserAuthService(IUsersRepository usersRepository, IPasswordHasherService passwordHasherService,
        ITokenService tokenService, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _passwordHasherService = passwordHasherService;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
    {
        var user = await _usersRepository.GetByUsernameAsync(loginRequestDto.Username);

        if (user is null || !_passwordHasherService.VerifyPassword(loginRequestDto.Password, user.PasswordHash))
            return Result<LoginResponseDto>.Failure(HttpStatusCode.BadRequest, "Invalid username or password");

        return Result<LoginResponseDto>.Success(new LoginResponseDto
            { Token = _tokenService.GenerateToken(user.Id, user.Username, user.Role) });
    }

    public async Task<Result<SignupResponseDto>> Signup(SignupRequestDto signupRequestDto)
    {
        var user = await _usersRepository.GetByUsernameAsync(signupRequestDto.Username);

        if (user is not null)
            return Result<SignupResponseDto>.Failure(HttpStatusCode.BadRequest, "User already exists");

        var newUser = _mapper.Map<SignupRequestDto, User>(signupRequestDto);

        newUser.PasswordHash = _passwordHasherService.HashPassword(signupRequestDto.Password);

        await _usersRepository.AddAsync(newUser);
        await _usersRepository.SaveChangesAsync();

        return Result<SignupResponseDto>.Success(new SignupResponseDto
            { Token = _tokenService.GenerateToken(newUser.Id, newUser.Username, newUser.Role) });
    }
}