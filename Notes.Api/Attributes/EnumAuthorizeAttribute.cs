using Microsoft.AspNetCore.Authorization;
using Notes.Domain.Enums;

namespace Notes.Api.Attributes;

public class EnumAuthorizeAttribute : AuthorizeAttribute
{
    public EnumAuthorizeAttribute(UserRole userRole)
    {
    }
}