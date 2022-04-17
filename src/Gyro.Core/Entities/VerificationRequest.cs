using System;
using Gyro.Domain.Shared;

namespace Gyro.Core.Entities
{
    public sealed class VerificationRequest : EntityBase
    {
        public VerificationRequest(int userId, VerificationType verificationType, Guid token)
        {
            UserId = userId;
            VerificationType = verificationType;
            Token = token;
        }
        
        public int UserId { get; set; }
        
        public User User { get; set; }

        public VerificationType VerificationType { get; set; }

        public DateTime ExpirationTime { get; set; }
        
        public DateTime? ActivationTime { get; set; }

        public Guid Token { get; set; }
    }
}