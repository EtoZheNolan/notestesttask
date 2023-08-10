namespace Notes.Application.DTOs.Requests;

public class CreateCategoryRequestDto
{
    public string Name { get; set; } = null!;

    public Guid? ParentCategoryId { get; init; }
}