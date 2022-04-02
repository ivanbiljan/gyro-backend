using System.Collections.Generic;
using System.Threading.Tasks;
using Gyro.Domain.Shared;

namespace Gyro.Domain.Entities
{
    public sealed class Project : AuditableEntityBase
    {
        public string Name { get; set; }
        
        public User Lead { get; set; }
        
        public List<Issue> Tasks { get; set; }
        
        public List<User> Members { get; set; }
    }
}