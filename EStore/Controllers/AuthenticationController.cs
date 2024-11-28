using EStore.Core.Models;
using EStore.Services.Authentication;
using EStore.Services.Helper.EmailHelper;
using EStore.Services.SharedDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationUserServices _authUserServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailHelper _emailHelper;

        public AuthenticationController(IAuthenticationUserServices authenticationUserServices,
            UserManager<ApplicationUser> userManager,IEmailHelper emailHelper)
        {
            this._authUserServices = authenticationUserServices;
            this._userManager = userManager;
            this._emailHelper = emailHelper;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto User)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }
            var result = await _authUserServices.RegisterAsync(User);
            if (!result.IsSucceed) 
            {
                return BadRequest(result);
            }
            var applicationUser=await _userManager.FindByNameAsync(User.UserName);
            await _emailHelper.SendEmailToConfirmRegisterAsync(applicationUser);
            return Ok(result);
        }
        [HttpGet("ConfirmRegister")]
        public async Task<IActionResult> ConfirmRegister(string UserName, string token) 
        {
            if(!ModelState.IsValid)
                return BadRequest();
            var UserMangerResponce =await _authUserServices.ConfirmRegisterAsync(UserName,token);
            if (UserMangerResponce.IsSucceed) 
                return Ok(UserMangerResponce);
            return BadRequest(UserMangerResponce);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto UserLoginDto) 
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result=await _authUserServices.LoginAsync(UserLoginDto);
            if (!result.IsSucceed)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("gettoken")]
        public async Task<IActionResult> GetNewToken(string UserId,string RefreshToken) 
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result=await _authUserServices.GenerateTokenWithRefreshToken(UserId, RefreshToken);
            if (!result.IsSucceed)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("ForgetPassword")]
        public async Task<IActionResult> ForgrtPassword([EmailAddress]string Email) 
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result=await _authUserServices.ForgetPasswordServiceAsync(Email);
            if (!result.IsSucceed)
                return BadRequest(result);
            return Ok(result);
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string Email, string token , [FromForm]string NewPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var result = await _authUserServices.ResetPasswordAsync(Email, token, NewPassword);
            if (!result.IsSucceed)
                return BadRequest(result);
            return Ok(result);
        }
    }
}

