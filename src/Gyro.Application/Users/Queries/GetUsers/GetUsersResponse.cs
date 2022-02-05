using System.Collections.Generic;
using JetBrains.Annotations;

namespace Gyro.Application.Users.Queries.GetUsers
{
    [PublicAPI]
    public sealed class GetUsersResponse
    {
        public IEnumerable<UserDto> Users { get; init; } = new List<UserDto>();
    }
}