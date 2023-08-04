using Notes.Domain.Enums;

namespace Notes.Application.Interfaces.InfrastructureServices;

public interface ITokenService
{
    string GenerateToken(Guid id, string username, UserRole role);
}