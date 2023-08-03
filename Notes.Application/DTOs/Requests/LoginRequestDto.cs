namespace Notes.Application.DTOs.Requests;

public class LoginRequestDto
{
    public string Username { get; init; } = null!;

    public string Password { get; init; } = null!;
}