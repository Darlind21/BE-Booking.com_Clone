using BookingClone.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookingClone.Infrastructure.Services.Email
{
    //FEATURE: IMPLEMENT SENDGRID
    public class EmailService(string smtpServer, int port, string fromEmail, string fromPassword) : IEmailService
    {
        private readonly string _smtpServer = smtpServer;
        private readonly int _port = port;
        private readonly string _fromEmail = fromEmail;
        private readonly string _fromPassword = fromPassword;

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using var client = new SmtpClient(_smtpServer, _port)
            {
                Credentials = new NetworkCredential(_fromEmail, _fromPassword),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail),
                Subject = subject,
                Body = body
            };

            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }
}
