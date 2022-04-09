using Gyro.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Infrastructure.Persistence
{
    internal static class DatabaseInitializer
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            var users = new User[]
            {
                new("Administrator", "admin@gyro.com", "Not a hashed password") { Id = 1 }
            };

            modelBuilder.Entity<User>().HasData(users);
        }
    }
}