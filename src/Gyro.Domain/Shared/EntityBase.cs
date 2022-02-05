using System;
using JetBrains.Annotations;

namespace Gyro.Domain.Shared
{
    [PublicAPI]
    public abstract class EntityBase
    {
        public DateTime CreatedAt { get; } = DateTime.Now;
        public int Id { get; private set; }
    }
}