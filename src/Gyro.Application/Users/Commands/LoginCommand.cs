using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Gyro.Application.Exceptions;
using Gyro.Application.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Application.Users.Commands
{
    public record LoginUserRequest(string Username, string Password) : IRequest<LoginUserResponse>;

    public record LoginUserResponse;

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
        private readonly IPasswordHasher _passwordHasher;

        public LoginCommand(IGyroContext db, IPasswordHasher passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
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
            
            if (!_passwordHasher.VerifyPassword(request.Password, user.HashedPassword))
            {
                throw new GyroException("Invalid password");
            }

            return new LoginUserResponse();
        }
    }
}