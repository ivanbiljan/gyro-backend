using System;
using System.ComponentModel.DataAnnotations.Schema;
using Gyro.Core.Shared;

namespace Gyro.Core.Entities;

public sealed class RefreshToken : AuditableEntityBase
{
    public string CreatedBy { get; set; }

    public DateTime ExpiresAt { get; set; }

    [NotMapped] public bool IsActive => !ArchiveDate.HasValue;

    [NotMapped] public bool IsRevoked => string.IsNullOrWhiteSpace(RevokedBy);

    public User Owner { get; set; }

    public int OwnerId { get; set; }

    public string? RevokedBy { get; set; }
    public string Token { get; set; }
}