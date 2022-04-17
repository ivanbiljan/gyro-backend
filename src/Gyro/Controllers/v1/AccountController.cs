using System.Threading.Tasks;
using Gyro.Core.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gyro.Controllers.v1
{
    [AllowAnonymous]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("[controller]")]
    public sealed partial class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<RegisterUserResponse> Register([FromForm] RegisterUserRequest request) =>
            await _mediator.Send(request);

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<LoginUserResponse> Login([FromForm] LoginUserRequest request) =>
            await _mediator.Send(request);

        [HttpPut("confirm/{token}")]
        public async Task<ConfirmRegistrationResponse> ConfirmRegistration(
            [FromRoute] string token) => await _mediator.Send(new ConfirmRegistrationRequest(token));
    }
}