namespace Gyro.Core.Entities;

/// <summary>
///     Represents a contract that describes an entity that may have an optional tenant associated with it. Filtering such
///     entities by tenant ID is optional.
/// </summary>
public interface IMayHaveTenant
{
    /// <summary>
    ///     Gets an optional integer that uniquely identifies the entity's tenant.
    /// </summary>
    int? TenantId { get; }
}