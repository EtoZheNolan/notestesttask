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

    public async Task<List<Category>> GetAllByUsernameAsync(string username, bool asNoTracking = false)
    {
        var set = asNoTracking ? _dataContext.Categories.AsNoTracking() : _dataContext.Categories;

        return await set.Include(x => x.Author).Where(x => x.Author.Username == username).ToListAsync();
    }
}