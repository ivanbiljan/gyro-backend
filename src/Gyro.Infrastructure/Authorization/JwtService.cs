﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Gyro.Configuration;
using Gyro.Core.Authorization;
using Gyro.Core.Entities;
using Gyro.Core.Exceptions;
using Gyro.Core.Shared;
using Gyro.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Gyro.Infrastructure.Authorization
{
    internal sealed class JwtService : IJwtService
    {
        private const int AccessTokenExpirationDays = 1;
        private const int RefreshTokenExpirationDays = 30;
        private readonly ICurrentUserService _currentUserService;
        private readonly IGyroContext _db;

        private readonly JwtSettings _jwtOptions;

        public JwtService(IOptions<JwtSettings> jwtOptions, ICurrentUserService currentUserService, IGyroContext db)
        {
            _jwtOptions = jwtOptions.Value;
            _currentUserService = currentUserService;
            _db = db;
        }

        public async Task RevokeAsync(string token)
        {
            var refreshToken = await _db.RefreshTokens
                .Where(t => t.Token == token)
                .SingleOrDefaultAsync();

            if (refreshToken is null)
            {
                throw new GyroException("Invalid token");
            }

            if (!refreshToken.IsRevoked)
            {
                throw new GyroException("The token has been revoked");
            }

            if (!refreshToken.IsActive)
            {
                throw new GyroException("This token has expired");
            }

            refreshToken.ArchiveDate = DateTime.UtcNow;
        }

        public Task<(string Token, string RefreshToken)> CreateTokenAsync(int userId)
        {
            var newAccessToken = CreateAccessToken(userId);
            var newRefreshToken = CreateRefreshToken(userId);

            return Task.FromResult((newAccessToken, newRefreshToken.Token));
        }

        public async Task<(string Token, string RefreshToken)> RefreshTokenAsync(string token)
        {
            var refreshToken = await _db.RefreshTokens
                .Where(t => t.Token == token)
                .SingleOrDefaultAsync();

            if (refreshToken == null)
            {
                throw new GyroException("Invalid token");
            }

            var ownerId = int.Parse(_currentUserService.UserId);
            if (refreshToken.Owner.Id != ownerId)
            {
                throw new GyroException("Refresh token does not belong to this user");
            }

            if (!refreshToken.IsActive)
            {
                throw new GyroException("The token has expired");
            }

            if (!refreshToken.IsRevoked)
            {
                throw new GyroException("This token has been revoked");
            }

            return await CreateTokenAsync(ownerId);
        }

        private string CreateAccessToken(int userId)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var securityToken = new JwtSecurityToken(
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Key)),
                    SecurityAlgorithms.HmacSha256),
                expires: DateTime.UtcNow.AddDays(AccessTokenExpirationDays),
                claims: new[]
                {
                    new Claim(ClaimTypes.Sid, userId.ToString())
                }
            );

            return jwtHandler.WriteToken(securityToken);
        }

        private static RefreshToken CreateRefreshToken(int userId)
        {
            return new RefreshToken
            {
                CreatedBy = userId.ToString(),
                ExpiresAt = DateTime.UtcNow.AddDays(RefreshTokenExpirationDays),
                OwnerId = userId,
                Token = Guid.NewGuid().ToString()
            };
        }
    }
}