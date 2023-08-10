using Notes.Domain.Entities;

namespace Notes.Application.Interfaces.Repositories;

public interface ICategoriesRepository : IBaseRepository<Category>
{
    public Task<List<Category>> GetAllByUserIdAsync(Guid userId, bool asNoTracking = false);
}