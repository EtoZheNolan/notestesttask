using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces.Repositories;
using Notes.Domain.Entities;

namespace Notes.Infrastructure.Repositories;

public class CategoriesRepository : BaseRepository<Category>, ICategoriesRepository
{
    private readonly DataContext _dataContext;

    public CategoriesRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<Category>> GetAllByUserIdAsync(Guid userId, bool asNoTracking = false)
    {
        var set = asNoTracking ? _dataContext.Categories.AsNoTracking() : _dataContext.Categories;

        return await set.Where(x => x.AuthorId == userId).ToListAsync();
    }
}