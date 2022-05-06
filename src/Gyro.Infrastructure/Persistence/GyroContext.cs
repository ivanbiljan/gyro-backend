using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gyro.Core.Entities;
using Gyro.Core.Shared;
using Gyro.Domain.Shared;
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

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public DbSet<VerificationRequest> VerificationRequests => Set<VerificationRequest>();

        public Task<int> SaveAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GyroContext).Assembly);
            
            DatabaseInitializer.SeedData(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is not IAuditableEntity auditableEntity)
                {
                    continue;
                }
                
                switch (entry.State)
                {
                    case EntityState.Modified:
                        auditableEntity.LastModifiedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        auditableEntity.ArchiveDate = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}