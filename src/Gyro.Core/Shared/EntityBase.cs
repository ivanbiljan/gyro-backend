using System;

namespace Gyro.Core.Shared
{
    public abstract class EntityBase
    {
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        
        public int Id { get; init; }
    }
}