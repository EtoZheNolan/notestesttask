using Notes.Domain.Enums;

namespace Notes.Application.DTOs.Requests;

public class UpdateUserRoleRequestDto
{
    public Guid UserId { get; init; }
    
    public UserRole UserRole { get; init; }
}