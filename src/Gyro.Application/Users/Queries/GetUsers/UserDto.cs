using System;
using Gyro.Application.Shared.AutoMapper;
using Gyro.Domain.Entities;
using JetBrains.Annotations;

namespace Gyro.Application.Users.Queries.GetUsers
{
    [PublicAPI]
    public sealed class UserDto : MapsTo<User>
    {
        public DateTime? ArchiveDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Email { get; set; } = default!;
        public int Id { get; set; }

        public string Username { get; set; } = default!;
    }
}