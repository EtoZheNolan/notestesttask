namespace Notes.Application.DTOs.Responses;

public class UserResponseDto
{
    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;

    public string Username { get; set; } = null!;

    public string Role { get; set; } = null!;
}