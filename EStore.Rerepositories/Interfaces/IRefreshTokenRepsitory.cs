using EStore.Core.Models;
namespace EStore.Repositories.Interfaces
{
    public interface IRefreshTokenRepsitory
    {
        Task<bool> AddRefreshTokenAsync(RefreshToken refreshToken);
        Task<bool> UpdateRefreshTokenAsync(RefreshToken refreshToken);
        Task<RefreshToken> GetRefreshTokenAsync(string UserId,string Token);
    }
}
