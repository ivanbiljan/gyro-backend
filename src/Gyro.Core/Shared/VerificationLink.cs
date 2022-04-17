using System;
using Gyro.Core.Entities;
using Microsoft.VisualBasic;

namespace Gyro.Core.Shared
{
    public sealed class VerificationLink
    {
        private const string RegistrationVerificationLink = "/account/confirm/{0}";
        private const string ForgotPasswordVerificationLink = "/account/resetpassword/{0}";

        private readonly VerificationType _verificationType;
        private readonly string _token;

        private VerificationLink()
        {
        }

        private VerificationLink(VerificationType verificationType, string token)
        {
            _verificationType = verificationType;
            _token = token;
        }

        public static VerificationLink For(VerificationType verificationType, string token)
        {
            return new VerificationLink(verificationType, token);
        }

        public override string ToString()
        {
            var link = _verificationType switch
            {
                VerificationType.Registration => RegistrationVerificationLink,
                VerificationType.ForgotPassword => ForgotPasswordVerificationLink,
                _ => throw new ArgumentOutOfRangeException()
            };

            return string.Format(link, _token);
        }
    }
}