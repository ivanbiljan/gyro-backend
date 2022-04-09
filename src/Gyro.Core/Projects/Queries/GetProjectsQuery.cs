using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Gyro.Core.Entities;
using Gyro.Core.Shared;
using Gyro.Core.Shared.AutoMapper;
using Gyro.Core.Users;
using Gyro.Core.Users.Queries;
using MediatR;

namespace Gyro.Core.Projects.Queries
{
    public record GetProjectsRequest : IRequest<GetProjectsResponse>;

    public record GetProjectsResponse
    {
        public IEnumerable<ProjectDto> Projects { get; init; } = new List<ProjectDto>();
    }

    public sealed class ProjectDto : MapsTo<Project>
    {
        public string Name { get; set; }
        
        public UserDto Lead { get; set; }
        
        public string Description { get; set; }
    }

    public sealed class GetProjectsQuery : IRequestHandler<GetProjectsRequest, GetProjectsResponse>
    {
        private readonly IConfigurationProvider _configurationProvider;
        private readonly IGyroContext _db;

        public GetProjectsQuery(IGyroContext db, IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
            _db = db;
        }

        public Task<GetProjectsResponse> Handle(GetProjectsRequest request, CancellationToken cancellationToken)
        {
            var projects = _db.Projects.ProjectTo<ProjectDto>(_configurationProvider);

            return Task.FromResult(new GetProjectsResponse
            {
                Projects = projects
            });
        }
    }
}