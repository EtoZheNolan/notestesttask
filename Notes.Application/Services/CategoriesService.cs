using System.Net;
using AutoMapper;
using Notes.Application.DTOs.Requests;
using Notes.Application.DTOs.Responses;
using Notes.Application.Interfaces.ApplicationServices;
using Notes.Application.Interfaces.Repositories;
using Notes.Application.Results;
using Notes.Domain.Entities;

namespace Notes.Application.Services;

public class CategoriesService : ICategoriesService
{
    private readonly ICategoriesRepository _categoriesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IMapper _mapper;

    public CategoriesService(ICategoriesRepository categoriesRepository, IUsersRepository usersRepository, IMapper mapper)
    {
        _categoriesRepository = categoriesRepository;
        _usersRepository = usersRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<CategoryResponseDto>>> GetAllCategoriesAsync()
    {
        var result = await _categoriesRepository.GetAllAsync();
        
        return Result<List<CategoryResponseDto>>.Success(_mapper.Map<List<Category>, List<CategoryResponseDto>>(result));
    }

    public async Task<Result<List<CategoryResponseDto>>> GetCategoriesByUsernameAsync(string username)
    {
        var result = await _categoriesRepository.GetAllByUsernameAsync(username);
        
        return Result<List<CategoryResponseDto>>.Success(_mapper.Map<List<Category>, List<CategoryResponseDto>>(result));
    }

    public async Task<Result<CreateCategoryResponseDto>> CreateAsync(CreateCategoryRequestDto createCategoryRequestDto)
    {
        var user = await _usersRepository.GetByIdAsync(createCategoryRequestDto.AuthorId);

        if (user is null)
            return Result<CreateCategoryResponseDto>.Failure(HttpStatusCode.NotFound, "User doesn't exist");

        var entity = _mapper.Map<CreateCategoryRequestDto, Category>(createCategoryRequestDto);

        await _categoriesRepository.AddAsync(entity);
        await _categoriesRepository.SaveChangesAsync();

        return Result<CreateCategoryResponseDto>.Success(new CreateCategoryResponseDto { Id = entity.Id });
    }
}