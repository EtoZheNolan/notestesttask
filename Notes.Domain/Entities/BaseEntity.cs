namespace Notes.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    
    public DateTime CreateAt { get; set; }
    
    public DateTime UpdatedAt { get; set; }

    public bool IsDeleted { get; set; }
}