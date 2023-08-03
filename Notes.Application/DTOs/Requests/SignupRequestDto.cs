namespace Notes.Application.DTOs.Requests;

public class SignupRequestDto
{
    public string Username { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string LastName { get; init; } = null!;
}