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
    private readonly IMapper _mapper;

    public CategoriesService(ICategoriesRepository categoriesRepository, IMapper mapper)
    {
        _categoriesRepository = categoriesRepository;
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

    public async Task<Result<bool>> CreateAsync(CreateCategoryRequestDto createCategoryRequestDto)
    {
        var entity = _mapper.Map<CreateCategoryRequestDto, Category>(createCategoryRequestDto);

        await _categoriesRepository.AddAsync(entity);
        await _categoriesRepository.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}