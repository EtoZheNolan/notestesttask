using Notes.Domain.Enums;

namespace Notes.Application.Interfaces.InfrastructureServices;

public interface ITokenService
{
    string GenerateToken(string username, UserRole role);
}