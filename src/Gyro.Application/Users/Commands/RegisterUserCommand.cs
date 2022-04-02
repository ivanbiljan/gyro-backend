using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Gyro.Application.Exceptions;
using Gyro.Application.Shared;
using Gyro.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Application.Users.Commands
{
    public record RegisterUserRequest
        (string Email, string Username, string Password, string? FirstName, string? LastName) : IRequest<RegisterUserResponse>;

    public record RegisterUserResponse; 

    public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserRequest>
    {
        private static readonly Regex EmailRegex = new(
            "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$");
        
        public RegisterUserCommandValidator()
        {
            RuleFor(r => r.Email).Matches(EmailRegex).WithMessage("Invalid email address");
            RuleFor(r => r.Username).NotEmpty().WithMessage("Username is empty");
            RuleFor(r => r.FirstName).NotEmpty().WithMessage("First name is empty");
            RuleFor(r => r.LastName).NotEmpty().WithMessage("Last name is empty");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Password is empty").MinimumLength(6)
                .WithMessage("Password must contain at least 6 characters");
        }
    }

    public sealed class RegisterUserCommand : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IGyroContext _db;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserCommand(IGyroContext db, IPasswordHasher passwordHasher)
        {
            _db = db;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _db.Users
                .Where(u => u.Email == request.Email)
                .SingleOrDefaultAsync(cancellationToken);
            
            if (user is not null)
            {
                throw new GyroException("User already exists");
            }

            var hashedPassword = _passwordHasher.Hash(request.Password);
            var newUser = new User(request.Username, request.Email, hashedPassword);
            
            _db.Users.Add(newUser);
            await _db.SaveAsync(cancellationToken);

            return new RegisterUserResponse();
        }
    }
}