using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces.Repositories;
using Notes.Domain.Entities;

namespace Notes.Infrastructure.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DataContext _dataContext;

    protected BaseRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
        => await _dataContext.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<TEntity>> GetAllAsync(bool asNoTracking = false)
        => await (asNoTracking
            ? _dataContext.Set<TEntity>().AsNoTracking().ToListAsync()
            : _dataContext.Set<TEntity>().ToListAsync());

    public async Task AddAsync(TEntity entity)
        => await _dataContext.AddAsync(entity);
    
    public void Delete(TEntity entity)
        => _dataContext.Remove(entity);
    
    public void SoftDelete(TEntity entity)
        => entity.IsDeleted = true;
    
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _dataContext.SaveChangesAsync(cancellationToken);
}