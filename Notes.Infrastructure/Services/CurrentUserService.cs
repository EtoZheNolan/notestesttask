using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Notes.Application.Interfaces.InfrastructureServices;
using Notes.Domain.Enums;

namespace Notes.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            return Guid.TryParse(
                _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
                    ?.Value, out var value)
                ? value
                : default(Guid?);
        }
    }

    public string? Username => _httpContextAccessor.HttpContext.User.Claims
        .FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

    public UserRole? UserRole
    {
        get
        {
            return Enum.TryParse<UserRole>(
                _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value,
                out var value)
                ? value
                : default(UserRole?);
        }
    }
}