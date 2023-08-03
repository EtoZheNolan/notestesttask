using AutoMapper;
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
        var result = _mapper.Map<List<Category>, List<CategoryResponseDto>>(await _categoriesRepository.GetAllAsync());
        return Result<List<CategoryResponseDto>>.Success(result);
    }


    public async Task<Result<List<CategoryResponseDto>>> GetCategoriesByUsernameAsync(string username)
    {
        var result = _mapper.Map<List<Category>, List<CategoryResponseDto>>(await _categoriesRepository.GetAllByUsernameAsync(username));
        return Result<List<CategoryResponseDto>>.Success(result);
    }
}