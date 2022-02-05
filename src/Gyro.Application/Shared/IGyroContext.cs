using System.Threading;
using System.Threading.Tasks;
using Gyro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Application.Shared
{
    public interface IGyroContext
    {
        DbSet<User> Users { get; }

        Task<int> SaveAsync(CancellationToken cancellationToken);
    }
}