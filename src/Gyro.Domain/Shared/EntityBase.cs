using System;
using JetBrains.Annotations;

namespace Gyro.Domain.Shared
{
    [PublicAPI]
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