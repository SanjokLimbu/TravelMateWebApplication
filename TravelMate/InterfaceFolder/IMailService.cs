using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace TravelMate.InterfaceFolder
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
    public class SendGridMailService : IMailService
    {
        private readonly IConfiguration _config;

        public SendGridMailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = _config["SendGridKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("annoyingthreat@gmail.com", "Limbuwan");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
