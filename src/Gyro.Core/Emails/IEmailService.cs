using System.Threading.Tasks;

namespace Gyro.Core.Emails
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string content);
    }
}