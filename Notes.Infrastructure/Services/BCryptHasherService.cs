using Notes.Application.Interfaces.InfrastructureServices;

namespace Notes.Infrastructure.Services;

public class BCryptHasherService : IPasswordHasherService
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}