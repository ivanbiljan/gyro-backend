using System;
using Gyro.Core.Shared;

namespace Gyro.Core.Entities;

public sealed class VerificationRequest : EntityBase
{
    public VerificationRequest(int userId, VerificationType verificationType)
    {
        UserId = userId;
        VerificationType = verificationType;
        Token = Guid.NewGuid();
    }
    
    public VerificationRequest(int userId, VerificationType verificationType, Guid token) : this(userId, verificationType)
    {
        Token = token;
    }

    public int UserId { get; set; }

    public User User { get; set; }

    public VerificationType VerificationType { get; set; }

    public DateTime ExpirationTime { get; set; }

    public DateTime? ActivationTime { get; set; }

    public Guid Token { get; set; }
}