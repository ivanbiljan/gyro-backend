using System.Threading.Tasks;
using Gyro.Core.Projects.Commands;
using Gyro.Core.Projects.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gyro.Controllers.v1;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class ProjectsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProjectsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<CreateProjectResponse> CreateProject([FromBody] CreateProjectRequest request) =>
        await _mediator.Send(request);

    [HttpGet]
    public async Task<GetProjectsResponse> GetAllProjects(GetProjectsRequest request) => await _mediator.Send(request);

    [HttpGet("{id}")]
    public async Task<GetProjectResponse> GetProjectById([FromRoute] int id) =>
        await _mediator.Send(new GetProjectRequest(id));
}