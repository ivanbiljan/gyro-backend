using System;
using Gyro.Core.Entities;
using Gyro.Core.Shared.AutoMapper;
using JetBrains.Annotations;

namespace Gyro.Core.Users.Queries;

[PublicAPI]
public sealed class UserDto : MapsTo<User>
{
    public DateTime? ArchiveDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public string Email { get; set; } = default!;

    public int Id { get; set; }

    public string Username { get; set; } = default!;
}