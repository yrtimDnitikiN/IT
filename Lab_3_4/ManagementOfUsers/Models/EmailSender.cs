using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ManagementOfUsers.Models
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var msg = new MailMessage(new MailAddress("your_email", "your_name"), new MailAddress(email));
            msg.Subject = subject;
            msg.Body = htmlMessage;
            msg.IsBodyHtml = true;
            var client = new SmtpClient("your_smptp");
            client.Credentials = new NetworkCredential("your_email", "your_password");
            client.EnableSsl = true;
            await client.SendMailAsync(msg);   
        }
    }
}
