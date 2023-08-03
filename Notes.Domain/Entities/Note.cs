namespace Notes.Domain.Entities;

public class Note : BaseEntity
{
    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public Guid CategoryId { get; set; }
    
    public Category Category { get; set; } = null!;
    
    public Guid AuthorId { get; set; }
    
    public User Author { get; set; } = null!;
}