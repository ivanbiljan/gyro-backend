namespace Gyro.Core.Emails
{
    public sealed class SendGridSettings
    {
        public string ApiKey { get; set; }
        
        public string FromEmail { get; set; }
        
        public string FromName { get; set; }
    }
}