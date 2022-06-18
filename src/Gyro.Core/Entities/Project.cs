using System;
using System.Collections.Generic;
using Gyro.Core.Shared;

namespace Gyro.Core.Entities;

public sealed class Project : AuditableEntityBase
{
    public Project(string name, int leadId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        LeadId = leadId;
    }

    public string Name { get; set; }

    public int LeadId { get; set; }

    public User Lead { get; set; }

    public string? Description { get; set; }

    public List<Issue> Tasks { get; set; }

    public List<User> Members { get; set; }
    
    public List<Epic> Epics { get; set; }
    
    public Organization Organization { get; set; }
}