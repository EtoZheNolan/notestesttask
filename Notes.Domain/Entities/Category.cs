namespace Notes.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;

    public Guid? ParentCategoryId { get; set; }
    
    public Category? ParentCategory { get; set; }
    
    public Guid AuthorId { get; set; }
    
    public User Author { get; set; } = null!;
    
    public ICollection<Category> SubCategories { get; set; } = new List<Category>();
    
    public ICollection<Note> Notes { get; set; } = new List<Note>();
}