using BackEnd.Models;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using System.Net;
using System.Net.Mail;
using System.Text;
using MailKit.Net.Smtp;

namespace BackEnd.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task SendEmail(EmailModel model)
        {
            var email = _configuration["EmailInfo:Email"];
            var pass = _configuration["EmailInfo:Password"];

            //var client = new SmtpClient("smtp.gmail.com", 587)
            //{
            //    EnableSsl = true,
            //    Credentials = new NetworkCredential(email, pass)
            //};

            //return client.SendMailAsync(new MailMessage
            //(
            //    email,
            //    model.To,
            //    model.Subject,
            //    model.Body
            //));

            var smtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587)
            {
                Port = 587,
                Credentials = new NetworkCredential(email, pass),
                EnableSsl = true,
            };

            return smtpClient.SendMailAsync(email, model.To, model.Subject, model.Body);

        }

        public Task SendEmail2(EmailModel model)
        {
            throw new NotImplementedException();
        }

        //public Task SendEmail2(EmailModel model)
        //{
        //    var email = _configuration["EmailInfo:Email"];
        //    var pass = _configuration["EmailInfo:Password"];

        //    StringBuilder template = new StringBuilder();
        //    template.AppendLine("from " + email);
        //    template.AppendLine("Name " + model.To);
        //    template.AppendLine(model.Body);

        //    var Mails = new MimeMessage();
        //    Mails.From.Add(MailboxAddress.Parse(email));
        //    Mails.To.Add(MailboxAddress.Parse(model.To));
        //    Mails.Subject = model.Subject;
        //    Mails.Body = new TextPart(TextFormat.Text) { Text = template.ToString() };

        //    using var smtp = new MailKit.Net.Smtp.SmtpClient();
        //    smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        //    smtp.Authenticate(email, pass);
        //    smtp.Send(Mails);
        //    smtp.Disconnect(true);
        //}
    }
}
