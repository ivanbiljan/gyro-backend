using System.Threading.Tasks;
using Gyro.Core.Users.Commands;
using Gyro.Core.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gyro.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/users")]
    public sealed partial class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<GetUsersResponse> GetAllUsers([FromQuery] GetUsersQuery request) => await _mediator.Send(request);

        [HttpGet]
        [Route("{id}")]
        public async Task<GetUserResponse> GetUserById([FromRoute] int id) => await _mediator.Send(new GetUserQuery { Id = id });

        [HttpPost]
        public async Task<RegisterUserResponse> Register([FromBody] RegisterUserRequest request) =>
            await _mediator.Send(request);
    }
}