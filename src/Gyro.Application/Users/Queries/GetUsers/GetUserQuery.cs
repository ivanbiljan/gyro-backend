using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Gyro.Application.Shared;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Application.Users.Queries.GetUsers
{
    public sealed class GetUserQuery : IRequest<GetUserResponse>
    {
        public int Id { get; init; }
    }

    public sealed class GetUserQueryHandler : IRequestHandler<GetUserQuery, GetUserResponse>
    {
        private readonly IGyroContext _db;
        private readonly IMapper _mapper;

        public GetUserQueryHandler(IGyroContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        
        public async Task<GetUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _db.Users.AsNoTracking()
                .Where(u => u.Id == request.Id)
                .SingleOrDefaultAsync(cancellationToken);
            
            if (user is null)
            {
                // TODO: Application exception
                throw new Exception();
            }

            return new GetUserResponse
            {
                User = _mapper.Map<UserDto>(user)
            };
        }
    }

    [PublicAPI]
    public sealed class GetUserResponse
    {
        public UserDto User { get; init; } = default!;
    }
}