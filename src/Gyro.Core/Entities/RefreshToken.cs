using System;
using System.ComponentModel.DataAnnotations.Schema;
using Gyro.Core.Shared;

namespace Gyro.Core.Entities;

public sealed class RefreshToken : AuditableEntityBase
{
    public string Token { get; set; }

    public DateTime ExpiresAt { get; set; }

    public string CreatedBy { get; set; }

    public string? RevokedBy { get; set; }

    public User Owner { get; set; }

    [NotMapped] public bool IsRevoked => string.IsNullOrWhiteSpace(RevokedBy);

    [NotMapped] public bool IsActive => !ArchiveDate.HasValue;

    public int OwnerId { get; set; }
}