using System.Threading;
using System.Threading.Tasks;
using Gyro.Core.Entities;
using Gyro.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Infrastructure.Persistence
{
    public sealed class GyroContext : DbContext, IGyroContext
    {
        public GyroContext(DbContextOptions<GyroContext> options) : base(options)
        {
        }

        public DbSet<Issue> Issues => Set<Issue>();

        public DbSet<Permission> Permissions => Set<Permission>();

        public DbSet<Priority> Priorities => Set<Priority>();

        public DbSet<Project> Projects => Set<Project>();

        public DbSet<Role> Roles => Set<Role>();
        
        public DbSet<User> Users => Set<User>();

        public Task<int> SaveAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GyroContext).Assembly);
            
            DatabaseInitializer.SeedData(modelBuilder);
        }
    }
}