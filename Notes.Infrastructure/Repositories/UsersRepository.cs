using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces.Repositories;
using Notes.Domain.Entities;

namespace Notes.Infrastructure.Repositories;

public class UsersRepository : BaseRepository<User>, IUsersRepository
{
    private readonly DataContext _dataContext;

    public UsersRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dataContext.Users.FirstOrDefaultAsync(x => x.Username == username);
    }
}