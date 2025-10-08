using TodoX.Application.Common.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace TodoX.Infrastructure.Identity;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContext;

    public UserContext(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public Guid GetUserId()
    {
        _ = Guid.TryParse(_httpContext.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
        return userId;
    }
}