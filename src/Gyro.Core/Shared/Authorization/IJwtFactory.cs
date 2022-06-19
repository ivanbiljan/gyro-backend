using System.Collections.Generic;
using System.Security.Claims;

namespace Gyro.Core.Shared.Authorization;

/// <summary>
///     Defines a contract that describes a JWT factory; a <see langword="class" /> that produces JWTs.
/// </summary>
public interface IJwtFactory
{
    string CreateJwt(IEnumerable<Claim> claims);
}