using System;
using System.Collections.Generic;
using Gyro.Domain.Shared;

namespace Gyro.Core.Entities
{
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
        
        public List<Project> Projects { get; set; } = null!;

        public List<Issue> AssignedIssues { get; set; } = null!;

        public List<Issue> ReportedIssues { get; set; } = null!;
    }
}