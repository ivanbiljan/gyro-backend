using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Gyro.Application.Shared;
using MediatR;

namespace Gyro.Application.Users.Commands
{
    public sealed class RegisterUserRequest : IRequest<RegisterUserResponse>
    {
        public string Email { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Password { get; set; }
    }

    public sealed class RegisterUserCommandValidator : AbstractValidator<RegisterUserRequest>
    {
        private static readonly Regex EmailRegex = new(
            "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$");
        
        public RegisterUserCommandValidator()
        {
            RuleFor(r => r.Email).Matches(EmailRegex).WithMessage("Invalid email address");
            RuleFor(r => r.FirstName).NotEmpty().WithMessage("First name is empty");
            RuleFor(r => r.LastName).NotEmpty().WithMessage("Last name is empty");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Password is empty").MinimumLength(6)
                .WithMessage("Password must contain at least 6 characters");
        }
    }

    public sealed class RegisterUserCommand : IRequestHandler<RegisterUserRequest, RegisterUserResponse>
    {
        private readonly IGyroContext _db;
        
        public Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
        {
        }
    }
    
    public sealed class RegisterUserResponse
    {
        public string Message { get; init; }
    }
}