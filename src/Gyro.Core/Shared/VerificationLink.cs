using System;
using Gyro.Core.Entities;

using static Gyro.Core.Shared.Constants;

namespace Gyro.Core.Shared;

public sealed class VerificationLink
{
    private readonly string _token;
    private readonly VerificationType _verificationType;

    private VerificationLink()
    {
    }

    private VerificationLink(VerificationType verificationType, string token)
    {
        _verificationType = verificationType;
        _token = token;
    }

    public static VerificationLink For(VerificationType verificationType, string token) =>
        new VerificationLink(verificationType, token);

    public override string ToString()
    {
        var link = _verificationType switch
        {
            VerificationType.Registration   => VerificationLinks.GetAccountConfirmationLink(_token),
            VerificationType.ForgotPassword => VerificationLinks.GetPasswordResetLink(_token),
            _                               => throw new ArgumentOutOfRangeException()
        };

        return link;
    }
}