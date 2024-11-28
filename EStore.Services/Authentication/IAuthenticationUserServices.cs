using EStore.Services.SharedDtos;
using EStore.Services.SharedResponces;
using System.ComponentModel.DataAnnotations;

namespace EStore.Services.Authentication
{
    public interface IAuthenticationUserServices
    {
        Task<ServiceResponce> RegisterAsync(RegisterDto User);
        Task<ServiceResponce> ConfirmRegisterAsync(string UserName, string token);
        Task<ServiceResponce> LoginAsync(LoginDto UserLoginDto);
        Task<ServiceResponce> ResetPasswordAsync([EmailAddress]string Email, string Token, string NewPassword);
        Task<ServiceResponce> ForgetPasswordServiceAsync([EmailAddress] string Email);
        Task<ServiceResponce> GenerateTokenWithRefreshToken(string UserId,string RefreshToken);
    }
}