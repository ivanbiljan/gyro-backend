using System;
using Gyro.Core.Shared;

namespace Gyro.Core.Entities;

public sealed class Issue : AuditableEntityBase, IMustHaveTenant
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime DueDate { get; set; }

    public User Assignee { get; set; }

    public User Reporter { get; set; }

    public Project Project { get; set; }

    public Priority Priority { get; set; }
    
    public Epic Epic { get; set; }
    public string TenantId { get; }
}