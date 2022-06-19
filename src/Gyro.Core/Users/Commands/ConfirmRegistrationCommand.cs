using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Gyro.Core.Exceptions;
using Gyro.Core.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gyro.Core.Users.Commands;

public sealed record ConfirmRegistrationRequest(string Token) : IRequest<ConfirmRegistrationResponse>;

public sealed record ConfirmRegistrationResponse;

public sealed class ConfirmRegistrationCommand
    : IRequestHandler<ConfirmRegistrationRequest, ConfirmRegistrationResponse>
{
    private readonly IGyroContext _db;

    public ConfirmRegistrationCommand(IGyroContext db)
    {
        _db = db;
    }

    public async Task<ConfirmRegistrationResponse> Handle(ConfirmRegistrationRequest request,
        CancellationToken cancellationToken)
    {
        var verificationRequest = await _db.VerificationRequests
            .Where(r => r.Token == Guid.Parse(request.Token))
            .SingleOrDefaultAsync(cancellationToken);

        if (verificationRequest is null || verificationRequest.ExpirationTime < DateTime.UtcNow)
        {
            throw new GyroException("Invalid request");
        }

        var user = await _db.Users
            .Where(u => u.Id == verificationRequest.UserId)
            .SingleAsync(cancellationToken);

        if (verificationRequest.ActivationTime != null || user.ActivationTime != null)
        {
            throw new GyroException("The account had already been activated");
        }

        verificationRequest.ActivationTime = DateTime.UtcNow;
        user.ActivationTime = DateTime.UtcNow;

        await _db.SaveAsync(cancellationToken);

        return new ConfirmRegistrationResponse();
    }
}