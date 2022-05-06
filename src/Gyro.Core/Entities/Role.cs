using System.Collections.Generic;
using Gyro.Core.Shared;

namespace Gyro.Core.Entities
{
    public sealed class Role : AuditableEntityBase
    {
        public string Name { get; set; }
        
        public string Description { get; set; }

        public List<Permission> Permissions { get; init; }
    }
}