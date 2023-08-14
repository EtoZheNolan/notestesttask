using Notes.Domain.Enums;

namespace Notes.Application.Interfaces.InfrastructureServices;

public interface ICurrentUserService
{
    Guid? UserId { get; }
    
    UserRole? UserRole { get; }
}