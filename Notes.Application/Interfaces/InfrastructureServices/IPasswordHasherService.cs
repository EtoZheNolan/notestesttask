namespace Notes.Application.Interfaces.InfrastructureServices;

public interface IPasswordHasherService
{
    string HashPassword(string password);
    
    bool VerifyPassword(string password, string hashedPassword);
}