using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace multiplixe.notificador.email.console
{
    public class SmtpService
    { 
        private SmtpSettings smtpSettings { get; }

        public SmtpService(SmtpSettings smtpSettings)
        {
            this.smtpSettings = smtpSettings;
        }

        public void Send(string email, string nome, string titulo, string html)
        {
            using (var client = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = smtpSettings.UserName,
                    Password = smtpSettings.Password
                };

                client.Credentials = credential;
                client.Host = smtpSettings.Host;
                client.Port = smtpSettings.Port;
                client.EnableSsl = false;

                using (var message = new MailMessage())
                {
                    message.From = new MailAddress(smtpSettings.UserName, smtpSettings.Name);

                    message.To.Add(new MailAddress(email, nome));

                    message.Subject = titulo;
                    message.Body = html;
                    message.IsBodyHtml = true;

                    client.Send(message);
                }
            }
        }
    }
}
