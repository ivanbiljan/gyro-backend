namespace Gyro.Core.Shared;

/// <summary>
///     Represents a contract that describes an entity that must have a tenant associated with it. These entities are
///     always filter by their tenant ID.
/// </summary>
public interface IMustHaveTenant
{
    /// <summary>
    ///     Gets an integer that uniquely identifies an entity's tenant.
    /// </summary>
    string TenantId { get; set; }
}