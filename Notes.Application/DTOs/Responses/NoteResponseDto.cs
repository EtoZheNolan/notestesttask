namespace Notes.Application.DTOs.Responses;

public class NoteResponseDto
{
    public Guid Id { get; set; }
    
    public string Title { get; init; } = null!;

    public string Content { get; init; } = null!;
}