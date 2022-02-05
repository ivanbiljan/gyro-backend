using System;
using JetBrains.Annotations;

namespace Gyro.Domain.Shared
{
    [PublicAPI]
    public abstract class AuditableEntityBase : EntityBase, IAuditableEntity
    {
        public DateTime? LastModifiedDate { get; set; }
        
        public DateTime? ArchiveDate { get; set; }
    }
}