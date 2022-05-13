namespace Gyro.Core.Shared;

public interface IPasswordHasher
{
    string Hash(string password);

    bool VerifyPassword(string providedPassword, string hashedPassword);
}