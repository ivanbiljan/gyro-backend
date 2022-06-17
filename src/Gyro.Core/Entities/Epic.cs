using System.Collections.Generic;
using Gyro.Core.Shared;

namespace Gyro.Core.Entities;

/// <summary>
/// Represents a large body of work broken down into a collection of smaller <see cref="Issue"/>s.
/// </summary>
public sealed class Epic : AuditableEntityBase
{
    /// <summary>
    /// Gets or sets the name that briefly describes work that must be completed.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the description that provides an in-depth explanation of the work that must be completed. 
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Gets or sets the project this <see cref="Epic"/> is associated with.
    /// </summary>
    public Project Project { get; set; }

    /// <summary>
    /// Gets all the <see cref="Issue"/>s under this <see cref="Epic"/>.
    /// </summary>
    public List<Issue> Issues { get; init; } = new();
}