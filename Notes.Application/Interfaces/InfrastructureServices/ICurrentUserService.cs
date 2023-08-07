using Notes.Domain.Enums;

namespace Notes.Application.Interfaces.InfrastructureServices;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    
    string? Username { get; }
    
    UserRole? UserRole { get; }
}