using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace leave_management.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings _email;

        public EmailSender(IOptions<EmailSettings> email)
        {
            _email = email.Value;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_email.SenderName, _email.SenderEmail));
            mimeMessage.To.Add(MailboxAddress.Parse(email));
            mimeMessage.Subject = subject;
            var builder = new BodyBuilder { HtmlBody = htmlMessage };
            mimeMessage.Body = builder.ToMessageBody();
            try
            {
                using var client = new SmtpClient();
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync(_email.MailServer, _email.MailPort, _email.UseSsl).ConfigureAwait(false);

                await client.AuthenticateAsync(_email.SenderEmail, _email.Password).ConfigureAwait(false);

                await client.SendAsync(mimeMessage).ConfigureAwait(false);

                await client.DisconnectAsync(true).ConfigureAwait(false);
            
            }catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }


        }
    }
}
