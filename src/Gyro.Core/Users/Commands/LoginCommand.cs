using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Gyro.Core.Exceptions;
using Gyro.Core.Shared;
using Gyro.Core.Shared.Authorization;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Core.Users.Commands
{
    public record LoginUserRequest(string Username, string Password) : IRequest<LoginUserResponse>;

    public record LoginUserResponse(string AccessToken, string RefreshToken);

    public sealed class LoginCommandValidator : AbstractValidator<LoginUserRequest>
    {
        public LoginCommandValidator()
        {
            RuleFor(r => r.Username).NotEmpty().WithMessage("Username is empty");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Password is empty");
        }
    }

    public sealed class LoginCommand : IRequestHandler<LoginUserRequest, LoginUserResponse>
    {
        private readonly IGyroContext _db;
        private readonly IJwtService _jwtService;
        private readonly IPasswordHasher _passwordHasher;

        public LoginCommand(IGyroContext db, IPasswordHasher passwordHasher, IJwtService jwtService)
        {
            _db = db;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
        }

        public async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .Where(u => u.Username == request.Username)
                .SingleOrDefaultAsync(cancellationToken);

            if (user == null)
            {
                throw new GyroException("User does not exist");
            }

            if (user.ActivationTime is null)
            {
                throw new GyroException("The account has not been activated");
            }
            
            if (!_passwordHasher.VerifyPassword(request.Password, user.HashedPassword))
            {
                throw new GyroException("Invalid password");
            }

            var (accessToken, refreshToken) = await _jwtService.CreateTokenAsync(user.Id);

            return new LoginUserResponse(accessToken, refreshToken);
        }
    }
}