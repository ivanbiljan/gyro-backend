using System;
using Gyro.Core.Entities;
using Gyro.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Infrastructure.Persistence;

internal static class DatabaseInitializer
{
    public static void SeedData(ModelBuilder modelBuilder, IPasswordHasher passwordHasher)
    {
        var organizations = new Organization[]
        {
            new("Gyro") { Id = 1 }
        };

        modelBuilder.Entity<Organization>().HasData(organizations);

        var users = new User[]
        {
            new("Administrator", "admin@gyro.com", passwordHasher.Hash("admin"))
            {
                Id = 1,
                ActivationTime = DateTime.UtcNow
            }
        };

        modelBuilder.Entity<User>().HasData(users);
    }
}