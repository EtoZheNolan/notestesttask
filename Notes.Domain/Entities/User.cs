using Notes.Domain.Enums;

namespace Notes.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
    
    public string FirstName { get; set; } = null!;
    
    public string LastName { get; set; } = null!;
    
    public UserRole Role { get; set; }

    public ICollection<Category> Categories { get; set; } = new List<Category>();

    public ICollection<Note> Notes { get; set; } = new List<Note>();
}