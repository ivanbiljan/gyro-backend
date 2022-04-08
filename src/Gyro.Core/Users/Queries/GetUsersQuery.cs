using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gyro.Core.Shared;
using JetBrains.Annotations;
using MediatR;

namespace Gyro.Core.Users.Queries
{
    public class GetUsersQuery : IRequest<GetUsersResponse>
    {
    }
    
    [PublicAPI]
    public sealed class GetUsersResponse
    {
        public IEnumerable<UserDto> Users { get; init; } = new List<UserDto>();
    }

    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, GetUsersResponse>
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IGyroContext _db;

        public GetUsersQueryHandler(IConfigurationProvider configurationProvider, IGyroContext db)
        {
            _configurationProvider = configurationProvider;
            _db = db;
        }

        public Task<GetUsersResponse> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var userDtos = _db.Users.ProjectTo<UserDto>(_configurationProvider);
            
            return Task.FromResult(new GetUsersResponse
            {
                Users = userDtos.AsEnumerable()
            });
        }
    }
}