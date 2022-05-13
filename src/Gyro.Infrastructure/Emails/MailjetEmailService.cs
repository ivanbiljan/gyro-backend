using System.Threading.Tasks;
using Gyro.Core.Shared.Emails;
using Mailjet.Client;
using Mailjet.Client.TransactionalEmails;
using Microsoft.Extensions.Options;

namespace Gyro.Infrastructure.Emails;

public sealed class MailjetEmailService : IEmailService
{
    private readonly IMailjetClient _mailjetClient;
    private readonly MailjetSettings _mailjetSettings;

    public MailjetEmailService(IOptions<MailjetSettings> mailjetSettings, IMailjetClient mailjetClient)
    {
        _mailjetClient = mailjetClient;
        _mailjetSettings = mailjetSettings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string content)
    {
        var email = new TransactionalEmailBuilder()
            .WithFrom(new SendContact(_mailjetSettings.FromEmail, _mailjetSettings.FromName))
            .WithTo(new SendContact(to))
            .WithSubject(subject)
            .WithTextPart(content)
            .Build();

        await _mailjetClient.SendTransactionalEmailAsync(email);
    }
}