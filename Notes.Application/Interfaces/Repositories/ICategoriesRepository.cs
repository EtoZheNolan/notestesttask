using Notes.Domain.Entities;

namespace Notes.Application.Interfaces.Repositories;

public interface ICategoriesRepository : IBaseRepository<Category>
{
    public Task<List<Category>> GetAllByUsernameAsync(string username, bool asNoTracking = false);
}