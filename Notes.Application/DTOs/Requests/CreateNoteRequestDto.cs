namespace Notes.Application.DTOs.Requests;

public class CreateNoteRequestDto
{
    public string Title { get; init; } = null!;

    public string Content { get; init; } = null!;

    public Guid? CategoryId { get; init; }

    public Guid AuthorId { get; init; }
}