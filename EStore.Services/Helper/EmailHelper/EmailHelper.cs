using EStore.Core.Models;
using EStore.Services.MailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace EStore.Services.Helper.EmailHelper
{
    public class EmailHelper:IEmailHelper
    {
        private readonly IMailService _mailService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        public EmailHelper(IMailService mailService, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _mailService = mailService;
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<bool> SendEmailToConfirmRegisterAsync(ApplicationUser applicationUser)
        {
            if (applicationUser is null)
                throw new NullReferenceException("User Can not be Null");
            var ConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
            if (ConfirmationToken is null)
                throw new InvalidOperationException
                    ($"Some thing Happend Pleas While Sending Confirmation Email to {applicationUser.UserName}");
            var encodedtoken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(ConfirmationToken));

            var confirmationLink = $"{_configuration["AppUrl"]}api/Authentication/ConfirmRegister?username={applicationUser.UserName}&token={encodedtoken}";

            var result = await _mailService.SendEmailAsync(applicationUser.Email, "Email Confirmation", $"<h1>Thanks {applicationUser.UserName}" +
                $"please click Here to Confirm You Password {confirmationLink}<h1>");
            if (!result)
                return false;

            return true;
        }

        public async Task<bool> SendResetPasswordUrlAsync(string Email,string Token)
        {
            var ResetPasswordUrl = $"{_configuration["AppUrl"]}api/Authentication/ResetPassword?Email={Email}&token={Token}";

            var result = await _mailService.SendEmailAsync(Email, "Reset Password", $"Please Click On this To reset Your Passwrd {ResetPasswordUrl}");
            if (!result)
                return false;
            return true;
        }
    }
}
