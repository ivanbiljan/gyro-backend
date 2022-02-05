using System;
using JetBrains.Annotations;

namespace Gyro.Domain.Shared
{
    [PublicAPI]
    public abstract class EntityBase
    {
        public int Id { get; private set; }

        public DateTime CreatedAt { get; } = DateTime.Now;
    }
}