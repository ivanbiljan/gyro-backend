namespace Gyro.Core.Shared;

public static class Constants
{
    public const string InvalidTenantId = nameof(InvalidTenantId);
    
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

    public static class Jwt
    {
        public const int AccessTokenExpirationDays = 1;
       
        public const int RefreshTokenExpirationDays = 30;
    }
}