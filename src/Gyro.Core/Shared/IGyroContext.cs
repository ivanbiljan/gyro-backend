﻿using System.Threading;
using System.Threading.Tasks;
using Gyro.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Core.Shared;

public interface IGyroContext
{
    DbSet<Epic> Epics { get; }

    DbSet<Issue> Issues { get; }

    DbSet<Permission> Permissions { get; }

    DbSet<Priority> Priorities { get; }

    DbSet<Project> Projects { get; }

    DbSet<Role> Roles { get; }

    DbSet<User> Users { get; }
    
    DbSet<UserAbout> UserAbouts { get; }

    DbSet<RefreshToken> RefreshTokens { get; }

    DbSet<VerificationRequest> VerificationRequests { get; }
    
    DbSet<Organization> Organizations { get; }

    Task<int> SaveAsync(CancellationToken cancellationToken);
}