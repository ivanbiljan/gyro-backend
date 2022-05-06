using System.Collections.Generic;
using Gyro.Core.Shared;

namespace Gyro.Core.Entities
{
    public sealed class Permission : EntityBase
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public List<Role> Roles { get; set; }
    }
}