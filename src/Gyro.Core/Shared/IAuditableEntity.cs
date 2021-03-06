using System;

namespace Gyro.Core.Shared;

/// <summary>
///     Defines a contract that describes an auditable entity.
/// </summary>
public interface IAuditableEntity
{
    DateTime? ArchiveDate { get; set; }

    DateTime? LastModifiedDate { get; set; }
}