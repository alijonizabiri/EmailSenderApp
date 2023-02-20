using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Options;
using WebApi.Models;

namespace WebApi.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<SMTPConfigModel> _smtpConfig;

        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig;
        }

        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            await SendEmail(userEmailOptions);
        }

        private async Task SendEmail(UserEmailOptions userEmail)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(_smtpConfig.Value.SenderAddress, _smtpConfig.Value.SenderDisplayName);
            mail.Subject = userEmail.Subject;
            mail.To.Add(new MailAddress(userEmail.To));
            mail.Body = userEmail.Body; 
            mail.IsBodyHtml = _smtpConfig.Value.IsBodyHtml;
            

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.Value.SenderAddress, _smtpConfig.Value.Password);

            var smtpClient = new System.Net.Mail.SmtpClient
            {
                Host = _smtpConfig.Value.Host,
                Port = _smtpConfig.Value.Port,
                EnableSsl = _smtpConfig.Value.EnableSSL,
                UseDefaultCredentials = _smtpConfig.Value.UseDefaultCredentials,
                Credentials = networkCredential
            };

            await smtpClient.SendMailAsync(mail);
        }



        // private readonly IConfiguration _config;

        // public EmailService(IConfiguration config)
        // {
        //     _config = config;
        // }
        // public void SendEmail(EmailDto emailDto)
        // {
        //     var email = new MimeMessage();
        //     email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
        //     email.To.Add(MailboxAddress.Parse(emailDto.To));
        //     email.Subject = emailDto.Subject;
        //     email.Body = new TextPart(TextFormat.Html) { Text = emailDto.Body};

        //     using var smtp = new SmtpClient();
        //     smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
        //     smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
        //     smtp.Send(email);
        //     smtp.Disconnect(true);
        // }
    }
}
