using System.Threading.Tasks;
using Gyro.Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gyro.Controllers.v1
{
    [ApiController]
    [Route("/api/{version:apiVersion}/users")]
    public sealed partial class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<GetUsersResponse> Get([FromQuery] GetUsersQuery request) => await _mediator.Send(request);

        [HttpGet]
        [Route("{id}")]
        public async Task<GetUserResponse> Get([FromRoute] int id) => await _mediator.Send(new GetUserQuery { Id = id });
    }
}