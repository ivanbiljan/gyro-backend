using System;
using System.Collections.Generic;
using Gyro.Core.Shared;

namespace Gyro.Core.Entities;

public sealed class User : AuditableEntityBase
{
    public User(string username, string email, string hashedPassword)
    {
        Username = username ?? throw new ArgumentNullException(nameof(username));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        HashedPassword = hashedPassword ?? throw new ArgumentNullException(nameof(hashedPassword));
    }

    public string Email { get; set; }

    public string? FirstName { get; set; }

    public string HashedPassword { get; set; }

    public string? LastName { get; set; }

    public string Username { get; set; }

    public DateTime? ActivationTime { get; set; }

    public UserAbout About { get; }

    public List<Project> Projects { get; init; } = null!;

    public List<Issue> AssignedIssues { get; init; } = null!;

    public List<Issue> ReportedIssues { get; init; } = null!;
}