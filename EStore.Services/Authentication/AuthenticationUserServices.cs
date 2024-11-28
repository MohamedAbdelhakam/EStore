using EStore.Core.Models;
using EStore.Services.MailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using EStore.Services.SharedDtos;
using EStore.Core.AppContexts;
using EStore.Services.SharedResponces;
using System.Security.Cryptography;
using EStore.Repositories.Interfaces;
using EStore.Services.Helper.EmailHelper;
using System.Net.Http.Headers;

namespace EStore.Services.Authentication
{
    public class AuthenticationUserServices : IAuthenticationUserServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMailService _mailService;
        private readonly IConfiguration _configuration;
        private ApplicationUser _user;
        private readonly AppDbContext _appDbContext;
        private readonly IUnitOfWork unitOfWork;
        private readonly IEmailHelper emailHelper;

        public AuthenticationUserServices(UserManager<ApplicationUser> userManager, IMailService mailService, IConfiguration configuration,
            AppDbContext appDbContext, IUnitOfWork unitOfWork, IEmailHelper emailHelper)

        {
            _userManager = userManager;
            _mailService = mailService;
            _configuration = configuration;
            _appDbContext = appDbContext;
            this.unitOfWork = unitOfWork;
            this.emailHelper = emailHelper;
        }
        #region Register
        public async Task<ServiceResponce> RegisterAsync(RegisterDto User)
        {
            if (User is null)
            {
                return new ServiceResponce
                {
                    Messege = "Missing User Data",
                    IsSucceed = false
                };
            }
            var NewUser = new ApplicationUser
            {
                FirstName = User.FirstName,
                LastName = User.LastName,
                Address = User.Address,
                UserName = User.UserName,
                Email = User.Email,
                PhoneNumber = User.PhoneNumber,
            };
            if (User.Role == "Administrator")
            {
                if (User.AdminId == null)
                    return new ServiceResponce
                    {
                        Messege = "Missing Admin Id",
                        IsSucceed = false
                    };

                var adminIdentitfier = _appDbContext.AdminIdentifiers.FirstOrDefault(ai => ai.AdminId == User.AdminId);
                if (adminIdentitfier == null)
                {
                    return new ServiceResponce
                    {
                        Messege = "Admin Id is Wrong Try Again",
                        IsSucceed = false
                    };
                }
                adminIdentitfier.AdminId = User.AdminId;


            }

            var result = await _userManager.CreateAsync(NewUser, User.Password);

            if (!result.Succeeded)
            {
                return new ServiceResponce
                {
                    Messege = "User Have not been Added ,May be Some Data Is Invalid",
                    IsSucceed = false,
                    Errors = result.Errors.Select(e => e.Description)
                };
            }
            try
            {

                await _userManager.AddToRoleAsync(NewUser, User.Role);
            }
            catch (Exception ex)
            {
                return new ServiceResponce
                {
                    Messege = ex.Message,
                    IsSucceed = false,
                };
            }
            return new ServiceResponce
            {
                Messege = "User Added Successfully And Have to Confirm Email",
                IsSucceed = true
            };
        }

        #endregion
        public async Task<ServiceResponce> ConfirmRegisterAsync(string UserName, string token)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(token))
            {
                return new ServiceResponce
                {
                    Messege = "Invalid Confirmation",
                    IsSucceed = false
                };
            }
            var user = await _userManager.FindByNameAsync(UserName);
            if (user is null)
                return new ServiceResponce
                {
                    Messege = "Invalid Data",
                    IsSucceed = false
                };
            var DecodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, DecodedToken);
            if (!result.Succeeded)
            {
                return new ServiceResponce
                {
                    Messege = "Invalid Confirmation",
                    IsSucceed = false
                };
            }
            return new ServiceResponce
            {
                Messege = "User Confirmed Successfully",
                IsSucceed = true
            };
        }

        #region Login
        public async Task<ServiceResponce> LoginAsync(LoginDto UserLoginDto)
        {
            var user = await _userManager.FindByEmailAsync(UserLoginDto.Email);
            if (user is null)
                return new ServiceResponce
                {
                    Messege = "User Or Password Wrong",
                    IsSucceed = false,
                };
            var result = await _userManager.CheckPasswordAsync(user, UserLoginDto.Password);
            if (!result)
                return new ServiceResponce
                {
                    Messege = "User Or Password Wrong",
                    IsSucceed = false,
                };
            if (!user.EmailConfirmed)
                return new ServiceResponce
                {
                    Messege = "Please Confirm Your Email First Check Your Email",
                    IsSucceed = false,
                };
            _user = user;

            (string, string) Tokens = await CreateToken();
            var AccsessToken = Tokens.Item1;
            var RefreshToken = Tokens.Item2;

            var HashedToken = HashToken(RefreshToken);
            var RefreshTokenEntity = new RefreshToken
            {
                UserId = _user.Id,
                Token = HashedToken,
                ExpirationTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshExp"])),
                IssuedTime = DateTime.UtcNow,
            };

            var Added = await unitOfWork.RefreshTokenRepsitory.AddRefreshTokenAsync(RefreshTokenEntity);

            if (!Added)
                return new ServiceResponce
                {
                    Messege = "Try Again",
                    IsSucceed = false,
                };
            unitOfWork.Complete();
            return new ServiceResponce
            {
                Messege = "User Is Valid",
                IsSucceed = true,
                Values =
                {
                    ["AccsessToken"]=AccsessToken,
                    ["RefreshToken"]=RefreshToken
                }
            };
        }
        #endregion

        #region TokenMethods
        private async Task<(string, string)> CreateToken()
        {
            var SigningCredentials = GetSigningCredentials();
            var Claims = await GetClaimsAsync();
            var JwtAccessSecurityToken = GenerateAccsessTokenOptions(SigningCredentials, Claims);

            var AccessToken = new JwtSecurityTokenHandler().WriteToken(JwtAccessSecurityToken);

            var RefreshToken = GenerateRefreshToken();

            return (AccessToken, RefreshToken);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var Key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var secret = new SymmetricSecurityKey(Key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }
        private async Task<List<Claim>> GetClaimsAsync()
        {
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,_user.Id),
                new Claim(ClaimTypes.Name,_user.UserName),
                new Claim("CartId",$"{_user.CartId}"),
            };
            var roles = await _userManager.GetRolesAsync(_user);
            foreach (var role in roles)
            {
                Claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return Claims;
        }

        private JwtSecurityToken GenerateAccsessTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var JwtSettings = _configuration.GetSection("Jwt");
            var tokenoptions = new JwtSecurityToken
                (
                    issuer: JwtSettings["Iss"],
                    audience: JwtSettings["Aud"],
                    claims: claims,
                    signingCredentials: signingCredentials,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(JwtSettings["AccsessExp"]))
                );
            return tokenoptions;
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        private string HashToken(string Token)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Token));
                return Convert.ToBase64String(hashedBytes);
            }
        }
        #endregion

        public async Task<ServiceResponce> GenerateTokenWithRefreshToken(string UserId, string RefreshToken)
        {
            var HashedToken = HashToken(RefreshToken);
            RefreshToken UserRefreshToken;
            try
            {
                UserRefreshToken = await unitOfWork.RefreshTokenRepsitory.GetRefreshTokenAsync(UserId, HashedToken);
            }
            catch (Exception ex)
            {
                return new ServiceResponce
                {
                    Messege = ex.Message,
                    IsSucceed = false
                };
            }

            if (UserRefreshToken is null)
            {
                return new ServiceResponce
                {
                    Messege = "the refresh Token of user May Wrong Or Changed",
                    IsSucceed = false
                };
            }
            if (UserRefreshToken.Expired)
                return new ServiceResponce
                {
                    Messege = "You Have To login",
                    IsSucceed = false
                };
            var user = await _userManager.FindByIdAsync(UserId);
            _user = user;
            //item 1 is access token and item 2 is refresh token 

            (string, string) Tokens = await CreateToken();
            var AccsessToken = Tokens.Item1;
            var NewRefreshTokenValue = Tokens.Item2;

            UserRefreshToken.Token = HashToken(NewRefreshTokenValue);
            UserRefreshToken.ExpirationTime = DateTime.UtcNow.AddDays(Convert.ToDouble(_configuration["Jwt:RefreshExp"]));
            UserRefreshToken.IssuedTime = DateTime.UtcNow;

            var Updated = await unitOfWork.RefreshTokenRepsitory.UpdateRefreshTokenAsync(UserRefreshToken);
            if (!Updated)
                return new ServiceResponce
                {
                    Messege = "Invalid Operation Try Again",
                    IsSucceed = false
                };
            unitOfWork.Complete();
            return new ServiceResponce
            {
                Messege = "User Is Valid",
                IsSucceed = true,
                Values =
                {
                    ["AccsessToken"]=AccsessToken,
                    ["RefreshToken"]=NewRefreshTokenValue
                }
            };
        }

        public async Task<ServiceResponce> ForgetPasswordServiceAsync (string Email)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user is null)
                return new ServiceResponce
                {
                    Messege = "new user Exist",
                    IsSucceed = false,
                };
            var token=await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodedtoken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var sent = await emailHelper.SendResetPasswordUrlAsync(Email,encodedtoken);
            if (!sent)
                return new ServiceResponce
                { 
                    Messege="Invalid Operation",
                    IsSucceed = false,
                };
            return new ServiceResponce
            {
                Messege="Check Email",
                IsSucceed = true,
            };
        }
        public async Task<ServiceResponce> ResetPasswordAsync(string Email, string Token,string NewPassword)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Token))
                return new ServiceResponce
                {
                    Messege="Invalid Data",
                    IsSucceed = false,
                };
            var user=await _userManager.FindByEmailAsync(Email);
            if (user is null)
                return new ServiceResponce
                {
                    Messege="No user With this email",
                    IsSucceed = false,
                };
            var DecodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(Token));
            var result =await _userManager.ResetPasswordAsync(user, DecodedToken, NewPassword);
            if (!result.Succeeded)
                return new ServiceResponce
                {
                    Messege="Invalid Operation",
                    IsSucceed = false,
                };
            return new ServiceResponce
            {
                Messege ="Password Updated",
                IsSucceed = true,
            };
        }
    }
}
