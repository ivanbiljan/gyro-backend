using System;
using JetBrains.Annotations;

namespace Gyro.Domain.Shared
{
    /// <summary>
    ///     Defines a contract that describes an auditable entity.
    /// </summary>
    [PublicAPI]
    public interface IAuditableEntity
    {
        DateTime? ArchiveDate { get; set; }
        DateTime? LastModifiedDate { get; set; }
    }
}