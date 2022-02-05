using System;
using JetBrains.Annotations;

namespace Gyro.Domain.Shared
{
    [PublicAPI]
    public abstract class AuditableEntityBase : EntityBase, IAuditableEntity
    {
        public DateTime? ArchiveDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}