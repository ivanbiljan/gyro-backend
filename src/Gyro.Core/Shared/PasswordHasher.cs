using Gyro.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Gyro.Core.Shared;

internal sealed class PasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<User> _passwordHasher = new();

    public bool VerifyPassword(string providedPassword, string hashedPassword) =>
        _passwordHasher.VerifyHashedPassword(null!, hashedPassword, providedPassword) ==
        PasswordVerificationResult.Success;

    public string Hash(string password) => _passwordHasher.HashPassword(null!, password);
}