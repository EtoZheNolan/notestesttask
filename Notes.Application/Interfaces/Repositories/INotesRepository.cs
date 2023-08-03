using Notes.Domain.Entities;

namespace Notes.Application.Interfaces.Repositories;

public interface INotesRepository : IBaseRepository<Note>
{
    Task<List<Note>> GetNotesByUsernameAsync(string username, bool asNoTracking = false);
}