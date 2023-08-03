using Notes.Domain.Entities;

namespace Notes.Application.Interfaces.Repositories;

public interface IUsersRepository : IBaseRepository<User>
{
    public Task<User?> GetByUsernameAsync(string username);
}