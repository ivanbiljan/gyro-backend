using System.Threading;
using System.Threading.Tasks;
using Gyro.Application.Shared;
using Gyro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Infrastructure.Persistence
{
    public sealed class GyroContext : DbContext, IGyroContext
    {
        public GyroContext(DbContextOptions<GyroContext> options) : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        
        public Task<int> SaveAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);
    }
}