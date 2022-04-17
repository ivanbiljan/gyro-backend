using System.Threading.Tasks;

namespace Gyro.Core.Shared.Authorization
{
    public interface IJwtService
    {
        Task<(string Token, string RefreshToken)> CreateTokenAsync(int userId);

        Task<(string Token, string RefreshToken)> RefreshTokenAsync(string token);

        Task RevokeAsync(string token);
    }
}