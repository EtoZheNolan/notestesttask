using Notes.Application.DTOs.Requests;
using Notes.Application.DTOs.Responses;
using Notes.Application.Results;

namespace Notes.Application.Interfaces.ApplicationServices;

public interface ICategoriesService
{
    Task<Result<List<CategoryResponseDto>>> GetAllCategoriesAsync();
    
    Task<Result<List<CategoryResponseDto>>> GetCategoriesByUserIdAsync(Guid userId);
    
    Task<Result<CreateCategoryResponseDto>> CreateAsync(Guid userId, CreateCategoryRequestDto createNoteRequestDto);
}