using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Gyro.Core.Entities;
using Gyro.Core.Exceptions;
using Gyro.Core.Shared;
using Gyro.Core.Shared.Emails;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Gyro.Core.Shared.Constants;

namespace Gyro.Core.Users.Commands;

public record RegisterUserRequest
(string Email, string Username, string Password, string? FirstName,
    string? LastName) : IRequest<RegisterUserResponse>;

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
    private readonly IEmailService _emailService;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommand(IGyroContext db, IPasswordHasher passwordHasher, IEmailService emailService)
    {
        _db = db;
        _passwordHasher = passwordHasher;
        _emailService = emailService;
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
        
        // TODO: handle work emails?
        // Keep this as is for the time being; i.e., create a random organization for each new account
        // Further down the road we should be able to associate accounts to a specific organization according to the domain
        // More importantly, we should expand upon the registration flow to redirect the user to a page that allows him to specify the organization's name

        var organization = new Organization(request.Username);
        _db.Organizations.Add(organization);

        var hashedPassword = _passwordHasher.Hash(request.Password);
        var newUser = new User(request.Username, request.Email, hashedPassword)
        {
            Organization = organization
        };

        _db.Users.Add(newUser);
        
        var verificationRequest = new VerificationRequest(newUser.Id, VerificationType.Registration)
        {
            ExpirationTime = DateTime.UtcNow.AddMinutes(VerificationLinks.AccountConfirmationExpirationMinutes)
        };

        _db.VerificationRequests.Add(verificationRequest);

        var verificationLink = VerificationLink.For(verificationRequest);
        await _emailService.SendEmailAsync(request.Email, "Confirm account",
            $"Confirm your account: {verificationLink}");

        await _db.SaveAsync(cancellationToken);

        return new RegisterUserResponse();
    }
}