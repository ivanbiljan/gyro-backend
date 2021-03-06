using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Gyro.Core.Entities;
using Gyro.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Gyro.Infrastructure.Persistence;

public sealed class GyroContext : DbContext, IGyroContext
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly string _tenantId;

    public GyroContext(DbContextOptions<GyroContext> options, ITenantResolver tenantResolver,
        IPasswordHasher passwordHasher) : base(options)
    {
        // TenantId can only be null in the case of an unauthorized call or a malicious attack
        _tenantId = tenantResolver.GetTenantId() ?? Constants.InvalidTenantId;
        _passwordHasher = passwordHasher;
    }

    public DbSet<Epic> Epics => Set<Epic>();

    public DbSet<Issue> Issues => Set<Issue>();

    public DbSet<Organization> Organizations => Set<Organization>();

    public DbSet<Permission> Permissions => Set<Permission>();

    public DbSet<Priority> Priorities => Set<Priority>();

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<Role> Roles => Set<Role>();

    public DbSet<User> Users => Set<User>();

    public DbSet<UserAbout> UserAbouts => Set<UserAbout>();

    public DbSet<VerificationRequest> VerificationRequests => Set<VerificationRequest>();

    public Task<int> SaveAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is IMustHaveTenant { TenantId: null } mustHaveTenant && entry.State == EntityState.Added)
            {
                if (_tenantId == Constants.InvalidTenantId)
                {
                    entry.State = EntityState.Detached;
                }

                mustHaveTenant.TenantId = _tenantId!;
            }

            if (entry.Entity is not IAuditableEntity auditableEntity)
            {
                continue;
            }

            switch (entry.State)
            {
                case EntityState.Modified:
                {
                    auditableEntity.LastModifiedDate = DateTime.UtcNow;
                }

                    break;
                case EntityState.Deleted:
                {
                    entry.State = EntityState.Modified;
                    auditableEntity.ArchiveDate = DateTime.UtcNow;
                }

                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GyroContext).Assembly);
        ConfigureGlobalFilters(modelBuilder);

        DatabaseInitializer.SeedData(modelBuilder, _passwordHasher);
    }

    private void ConfigureGlobalFilters(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            ConfigureTenantFilter(entityType);
            ConfigureArchivedFilter(entityType);
        }

        void ConfigureTenantFilter(IMutableEntityType entityType)
        {
            if (!typeof(IMustHaveTenant).IsAssignableFrom(entityType.ClrType))
            {
                return;
            }

            var entity = Expression.Parameter(entityType.ClrType, "e");
            var tenantId = Expression.PropertyOrField(entity, nameof(IMustHaveTenant.TenantId));
            var filter =
                Expression.Lambda(Expression.Equal(tenantId, Expression.Constant(_tenantId)),
                    entity);

            entityType.SetQueryFilter(filter);
        }

        void ConfigureArchivedFilter(IMutableEntityType entityType)
        {
            if (!typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
            {
                return;
            }

            var entity = Expression.Parameter(entityType.ClrType, "e");
            var archiveDate = Expression.PropertyOrField(entity, nameof(IAuditableEntity.ArchiveDate));
            var filter =
                Expression.Lambda(Expression.Equal(archiveDate, Expression.Constant(null)),
                    entity);

            entityType.SetQueryFilter(filter);
        }
    }
}