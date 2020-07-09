using SendGrid;
using SendGrid.Helpers.Mail;
using System.Configuration;
using System.Threading.Tasks;

namespace PW.Interface
{
    public interface IMailService
    {
        Task SendEmailAsync(string toEmail, string subject, string content);
    }
    public class SendGridMailService : IMailService
    {
        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var apiKey = ConfigurationManager.AppSettings["SendGrid"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("annoyingthreat@gmail.com", "Limbuwan");
            var to = new EmailAddress(toEmail);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
