using System.Threading.Tasks;
using Gyro.Core.Users.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gyro.Controllers.v1;

[AllowAnonymous]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("confirm/{token}")]
    public async Task<ConfirmRegistrationResponse> ConfirmRegistration(
        [FromRoute] string token) => await _mediator.Send(new ConfirmRegistrationRequest(token));

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<LoginUserResponse> Login([FromForm] LoginUserRequest request) =>
        await _mediator.Send(request);

    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<RegisterUserResponse> Register([FromForm] RegisterUserRequest request) =>
        await _mediator.Send(request);
}