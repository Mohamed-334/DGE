using DGE.Domain.Shared.EmailModels;
using DGE.Infrastructure.Shared.Localization;
using DGE.Service.ServiceInterfaces;
using DGE.Service.Shared.Email;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace DGE.Service.Service
{
    public class EmailService : IEmailService
    {
        #region Fields
        private readonly EmailSettings _emailSettings;
        private readonly SmtpConnection _smtpConnection;
        private readonly IStringLocalizer<AppLocalization> _stringLocalizer;
        private readonly ILogger<EmailService> _logger;
        #endregion
        #region Constructors
        public EmailService(EmailSettings emailSettings, SmtpConnection smtpConnection, IStringLocalizer<AppLocalization> stringLocalizer, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings;
            _smtpConnection = smtpConnection;
            _stringLocalizer = stringLocalizer;
            _logger = logger;
        }

        #endregion
        #region Handle Functions
        public async Task<string> SendEmailAsync(string email, string Message, string? Subject)
        {
            try
            {
                var bodybuilder = new BodyBuilder
                {
                    HtmlBody = $"{Message}",
                };
                var message = new MimeMessage
                {
                    Body = bodybuilder.ToMessageBody()
                };
                message.From.Add(new MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
                message.To.Add(new MailboxAddress("testing", email));
                message.Subject = Subject == null ? "" : Subject;
                await _smtpConnection.SendAsync(message);
                return _stringLocalizer[AppLocalizationKeys.Success];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {Email}", email);
                return _stringLocalizer[AppLocalizationKeys.SendEmailFailed];
            }
        }
        #endregion
    }
}
