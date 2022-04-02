using System.Threading;
using System.Threading.Tasks;
using Gyro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Application.Shared
{
    public interface IGyroContext
    {
        DbSet<Issue> Issues { get; }
        
        DbSet<Permission> Permissions { get; }
        
        DbSet<Priority> Priorities { get; }
        
        DbSet<Project> Projects { get; }
        
        DbSet<Role> Roles { get; }

        DbSet<User> Users { get; }

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}