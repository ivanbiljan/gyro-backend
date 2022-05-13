using System.Security.Claims;
using Gyro.Core.Shared;
using Microsoft.AspNetCore.Http;
using static Gyro.Core.Shared.Constants;

namespace Gyro.Services;

internal sealed class ClaimsTenantResolver : ITenantResolver
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimsTenantResolver(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetTenantId() => _httpContextAccessor.HttpContext?.User?.FindFirstValue(Claims.TenantId);
}