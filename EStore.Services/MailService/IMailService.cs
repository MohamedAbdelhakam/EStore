using Microsoft.AspNetCore.Http;

namespace EStore.Services.MailService
{
    public interface IMailService
    {
        Task<bool> SendEmailAsync(string To, string Subject, string Body, IList<IFormFile> Attachments =null);
    }
}
