namespace Notes.Application.DTOs.Responses;

public class CategoryResponseDto
{
    public Guid Id { get; init; }
    
    public string Name { get; set; } = null!;

    public Guid? ParentCategoryId { get; init; }
    
    public Guid AuthorId { get; init; }
}