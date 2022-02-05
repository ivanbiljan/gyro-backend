using System.Threading.Tasks;
using Gyro.Application.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gyro.Controllers.v1
{
    [ApiController]
    [Route("/api/v1/users")]
    public sealed class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<GetUsersResponse> Get([FromQuery] GetUsersQuery request) => await _mediator.Send(request);
    }
}