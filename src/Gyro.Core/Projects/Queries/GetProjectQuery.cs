using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Gyro.Core.Entities;
using Gyro.Core.Exceptions;
using Gyro.Core.Shared;
using Gyro.Core.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Core.Projects.Queries;

public sealed record GetProjectRequest(int ProjectId) : IRequest<GetProjectResponse>;

public sealed record GetProjectResponse(ProjectDto ProjectDto);

public sealed class GetProjectQuery : IRequestHandler<GetProjectRequest, GetProjectResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IGyroContext _db;
    private readonly IMapper _mapper;

    public GetProjectQuery(IGyroContext db, IMapper mapper, ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
        _db = db;
        _mapper = mapper;
    }

    public async Task<GetProjectResponse> Handle(GetProjectRequest request, CancellationToken cancellationToken)
    {
        var userId = int.Parse(_currentUserService.UserId);
        var project = await _db.Projects
            .Where(p => p.Id == request.ProjectId)
            .Where(p => p.Lead.Id == userId)
            .Where(p => p.Members.Any(m => m.Id == userId))
            .FirstOrDefaultAsync(cancellationToken);

        if (project is null)
        {
            throw new GyroException("Project does not exist");
        }

        return new GetProjectResponse(_mapper.Map<Project, ProjectDto>(project));
    }
}