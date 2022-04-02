using Gyro.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Gyro.Application.Shared
{
    internal sealed class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<User> _passwordHasher = new();

        public string Hash(string password) => _passwordHasher.HashPassword(null!, password);

        public bool VerifyPassword(string providedPassword, string hashedPassword) =>
            _passwordHasher.VerifyHashedPassword(null!, hashedPassword, providedPassword) ==
            PasswordVerificationResult.Success;
    }
}