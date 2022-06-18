namespace Gyro.Core.Shared;

public static class Constants
{
    public static class Claims
    {
        public const string Sid = nameof(Sid);

        public const string TenantId = nameof(TenantId);
    }

    public static class VerificationLinks
    {
        public static string GetAccountConfirmationLink(string token) => $"/account/confirm/{token}";

        public const int AccountConfirmationExpirationMinutes = 15;

        public static string GetPasswordResetLink(string token) => $"/account/reset-password/{token}";
    }
}