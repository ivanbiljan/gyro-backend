using System.Threading.Tasks;
using Gyro.Core.Emails;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Gyro.Infrastructure.Emails
{
    public sealed class SendGridEmailService : IEmailService
    {
        private readonly ISendGridClient _sendGridClient;
        private readonly SendGridSettings _sendGridSettings;

        public SendGridEmailService(ISendGridClient sendGridClient, IOptions<SendGridSettings> sendGridSettings)
        {
            _sendGridClient = sendGridClient;
            _sendGridSettings = sendGridSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string content)
        {
            var sendGridMessage = new SendGridMessage
            {
                From = new EmailAddress(_sendGridSettings.FromEmail, _sendGridSettings.FromName),
                Subject = subject,
                PlainTextContent = content
            };
            
            sendGridMessage.AddTo(to);

            await _sendGridClient.SendEmailAsync(sendGridMessage);
        }
    }
}