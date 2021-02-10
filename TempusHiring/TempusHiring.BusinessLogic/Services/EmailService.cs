using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using TempusHiring.BusinessLogic.Interfaces;

namespace TempusHiring.BusinessLogic.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _cfg;

        public EmailService(IConfiguration configuration)
        {
            _cfg = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress(_cfg["SmtpConfig:SenderName"], _cfg["SmtpConfig:SenderMail"]));
            emailMessage.To.Add(new MailboxAddress(string.Empty, email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_cfg["SmtpConfig:Host"], Int32.Parse(_cfg["SmtpConfig:Port"]), true);
                await client.AuthenticateAsync(_cfg["SmtpConfig:SenderMail"], _cfg["SmtpConfig:Password"]);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}