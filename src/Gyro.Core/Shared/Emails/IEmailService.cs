using System.Threading.Tasks;

namespace Gyro.Core.Shared.Emails
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string content);
    }
}