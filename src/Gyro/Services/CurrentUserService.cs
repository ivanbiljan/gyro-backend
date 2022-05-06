using System.Security.Claims;
using Gyro.Core.Users;
using Microsoft.AspNetCore.Http;
using static Gyro.Core.Shared.Constants;

namespace Gyro.Services;

internal sealed class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?.User.FindFirstValue(Claims.Sid);
}