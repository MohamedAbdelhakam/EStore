using EStore.Core.AppContexts;
using EStore.Core.Models;
using EStore.Repositories.Interfaces;

namespace EStore.Repositories.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepsitory
    {
        private readonly AppDbContext _appDbContext;

        public RefreshTokenRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task<bool> AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            var RefreshToken=_appDbContext.RefreshTokens.Add(refreshToken);
            return true;
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string UserId, string Token)
        {
            var RefreshToken = _appDbContext.RefreshTokens.Where(rt=>rt.UserId==UserId).
                FirstOrDefault(rt=>rt.Token==Token);
            return RefreshToken;
        }

        public async Task<bool> UpdateRefreshTokenAsync(RefreshToken refreshToken)
        {
            _appDbContext.RefreshTokens.Update(refreshToken);
            return true;
        }
    }
}