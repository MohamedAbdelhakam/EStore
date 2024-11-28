using EStore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services.Helper.EmailHelper
{
    public interface IEmailHelper
    {
         Task<bool> SendEmailToConfirmRegisterAsync(ApplicationUser applicationUser);
        Task<bool> SendResetPasswordUrlAsync(string Email, string Token);
    }
}
