using System.Threading.Tasks;
using Gyro.Core.Projects.Commands;
using Gyro.Core.Projects.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gyro.Controllers.v1
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public sealed class ProjectsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<GetProjectsResponse> GetAll(GetProjectsRequest request) => await _mediator.Send(request);

        [HttpPost]
        public async Task<CreateProjectResponse> CreateProject(CreateProjectRequest request) =>
            await _mediator.Send(request);
    }
}