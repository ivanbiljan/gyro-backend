namespace Gyro.Core.Shared;

/// <summary>
/// Represents a contract that describes a tenant resolver.
/// </summary>
public interface ITenantResolver
{
    /// <summary>
    /// Resolves the user's current tenant ID.
    /// </summary>
    /// <returns>The tenant ID, or <see langword="null"/> if no tenant exists.</returns>
    string? GetTenantId();
}