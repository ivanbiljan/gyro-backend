using System;

namespace Gyro.Core.Shared;

public abstract class AuditableEntityBase : EntityBase, IAuditableEntity
{
    public DateTime? ArchiveDate { get; set; }

    public DateTime? LastModifiedDate { get; set; }
}