using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Gyro.Core.Shared.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static Gyro.Core.Shared.Constants;

namespace Gyro.Infrastructure.Authorization;

internal sealed class JwtFactory : IJwtFactory
{
    private readonly JwtSettings _jwtOptions;

    public JwtFactory(IOptions<JwtSettings> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string CreateJwt(IEnumerable<Claim> claims)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var securityToken = new JwtSecurityToken(
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Key)),
                SecurityAlgorithms.HmacSha256),
            expires: DateTime.UtcNow.AddDays(Jwt.AccessTokenExpirationDays),
            claims: claims
        );

        return jwtHandler.WriteToken(securityToken);
    }
}