using Microsoft.EntityFrameworkCore;
using Notes.Application.Interfaces.Repositories;
using Notes.Domain.Entities;

namespace Notes.Infrastructure.Repositories;

public class NotesRepository : BaseRepository<Note>, INotesRepository
{
    private readonly DataContext _dataContext;

    public NotesRepository(DataContext dataContext) : base(dataContext)
    {
        _dataContext = dataContext;
    }

    public async Task<List<Note>> GetNotesByUsernameAsync(string username, bool asNoTracking)
    {
        var set = asNoTracking ? _dataContext.Notes.AsNoTracking() : _dataContext.Notes;
        
        return await set.Include(x => x.Author)
            .Where(x => x.Author.Username == username).ToListAsync();
    }
}