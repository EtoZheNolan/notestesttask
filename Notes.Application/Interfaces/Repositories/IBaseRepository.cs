using Notes.Domain.Entities;

namespace Notes.Application.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(Guid id);

    Task<List<TEntity>> GetAllAsync(bool asNoTracking = false);

    Task AddAsync(TEntity entity);

    void Delete(TEntity entity);

    void SoftDelete(TEntity entity);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}