using System;

namespace Gyro.Domain.Shared
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            CreatedAt = DateTime.UtcNow;
        }
        
        public DateTime CreatedAt { get; }
        public int Id { get; init; }
    }
}