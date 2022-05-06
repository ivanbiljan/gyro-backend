namespace Gyro.Core.Users;

public interface ICurrentUserService
{
    string? UserId { get; }
}