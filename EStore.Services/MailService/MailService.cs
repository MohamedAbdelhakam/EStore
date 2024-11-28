using settings;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net.Sockets;
using System.Security.Authentication;
using MailKit.Security;

namespace EStore.Services.MailService
{
    public class MailService :IMailService
    {
        private readonly MailSettings _mailSettings;

        public MailService(IOptions<MailSettings> mailSettings)
        {
            this._mailSettings = mailSettings.Value;
        }
        public async Task<bool> SendEmailAsync(string To, string Subject, string Body, IList<IFormFile> Attachments = null)
        {
            try
            {
                var Email = new MimeMessage
                {
                    Sender = MailboxAddress.Parse(_mailSettings.Email),
                    Subject = Subject
                };

                Email.To.Add(MailboxAddress.Parse(To));

                var BodyBuilder = new BodyBuilder();

                if (Attachments != null)
                {
                    long MaxFileSize = 10 * 1024 * 1024; // 10 MB
                    foreach (var file in Attachments)
                    {
                        if (file != null && file.Length > 0)
                        {
                            if (file.Length > MaxFileSize)
                            {
                                throw new InvalidOperationException($"The File {file.FileName} exceeds the maximum allowed size of 10 MB.");
                            }

                            using var filestream = file.OpenReadStream();
                            BodyBuilder.Attachments.Add(file.FileName, filestream, ContentType.Parse(file.ContentType));
                        }
                    }
                }

                BodyBuilder.HtmlBody = Body;
                Email.Body = BodyBuilder.ToMessageBody();

                // Use a proper "From" address if needed, or validate this
                Email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

                using SmtpClient smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                var login = new SaslMechanismLogin(_mailSettings.UserName,_mailSettings.Password);
                await smtp.AuthenticateAsync(login);
                await smtp.SendAsync(Email);
                await smtp.DisconnectAsync(true);

                return true;
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Invalid email format: {ex.Message}");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"File I/O error: {ex.Message}");
            }

            catch (SmtpCommandException ex)
            {
                Console.WriteLine($"SMTP command error: {ex.Message}");
            }
            catch (SmtpProtocolException ex)
            {
                Console.WriteLine($"SMTP protocol error: {ex.Message}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Network error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            return false;
        }

    }
}
