namespace DGE.Domain.Shared.EmailModels
{
    public class EmailSettings
    {
        public int Port { get; set; }
        public string Host { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
        public string Password { get; set; }
        // Fallback inbox for new contact-us messages when CompanySettings has none; FromEmail is used when both are empty
        public string? NotificationEmail { get; set; }
    }
}
