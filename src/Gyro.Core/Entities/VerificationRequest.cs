using System;
using Gyro.Domain.Shared;

namespace Gyro.Core.Entities
{
    public sealed class VerificationRequest : EntityBase
    {
        public User User { get; set; }

        public VerificationType VerificationType { get; set; }

        public DateTime ExpirationTime { get; set; }

        public Guid Token { get; set; }
    }
}