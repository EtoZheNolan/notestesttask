using Notes.Application.DTOs.Responses;
using Notes.Application.Results;

namespace Notes.Application.Interfaces.ApplicationServices;

public interface ICategoriesService
{
    Task<Result<List<CategoryResponseDto>>> GetAllCategoriesAsync();
    
    Task<Result<List<CategoryResponseDto>>> GetCategoriesByUsernameAsync(string username);
}