using System.Security.Claims;
using AuthUserServiceApplication.Interfaces;
using AuthUserServiceDomain.Enums;

namespace AuthUserServiceApi.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    public int Id
    {
        get
        {
            var userId = _httpContextAccessor
                .HttpContext?
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            return int.Parse(userId!);
        }
    }


    public bool IsAdmin =>
        _httpContextAccessor
            .HttpContext!
            .User
            .IsInRole(nameof(Roles.Admin));
}