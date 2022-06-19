using System;
using System.Collections.Generic;
using Gyro.Core.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Gyro.Core.Entities;

/// <summary>
/// Represents an organization.
/// </summary>
public sealed class Organization : AuditableEntityBase, IMustHaveTenant
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Organization"/> with the specified organization name.
    /// </summary>
    /// <param name="name">The name, which must not be <see langword="null"/>.</param>
    /// <exception cref="ArgumentNullException">The <paramref name="name"/> was <see langword="null"/>.</exception>
    public Organization(string name)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
    
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets an in-depth explanation of what this organization does.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets the company associated with this organization.
    /// </summary>
    public string? Company { get; set; }
    
    /// <summary>
    /// Gets or sets the organization's email.
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Gets or sets the organization's location.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// Gets the organization's members.
    /// </summary>
    public List<User> Members { get; init; } = new();
    
    /// <summary>
    /// Gets the organization's projects.
    /// </summary>
    public List<Project> Projects { get; init; } = new();

    public string TenantId { get; }
}