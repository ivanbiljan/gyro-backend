using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Gyro.Configuration;
using Gyro.Core.Entities;
using Gyro.Core.Exceptions;
using Gyro.Core.Shared;
using Gyro.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Gyro.Core.Authorization
{
    public interface IJwtService
    {
        Task<(string Token, string RefreshToken)> CreateTokenAsync(int userId);

        Task<(string Token, string RefreshToken)> RefreshTokenAsync(string token);

        Task RevokeAsync(string token);
    }
}