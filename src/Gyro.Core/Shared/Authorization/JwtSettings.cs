namespace Gyro.Core.Shared.Authorization;

public sealed class JwtSettings
{
    public string Audience { get; init; }

    public string Issuer { get; init; }
    public string Key { get; init; }
}