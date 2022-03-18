using System;
using Gyro.Domain.Shared;

namespace Gyro.Domain.Entities
{
    public sealed class GyroIssue : AuditableEntityBase
    {
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public DateTime DueDate { get; set; }
        
        public User Asignee { get; set; }
        
        public User Reporter { get; set; }
        
        public Project Project { get; set; }
        
        public Priority Priority { get; set; }
    }
}