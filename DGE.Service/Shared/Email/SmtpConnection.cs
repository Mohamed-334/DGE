using DGE.Domain.Shared.EmailModels;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;

namespace DGE.Service.Shared.Email
{
    // Registered as a singleton: keeps one authenticated SMTP connection open and reuses it for every email,
    // instead of paying the TCP + TLS handshake and login on each send
    public sealed class SmtpConnection : IDisposable
    {
        #region Fields
        private readonly EmailSettings _emailSettings;
        private readonly SmtpClient _client = new();
        // SmtpClient is not thread-safe, so sends go through the connection one at a time
        private readonly SemaphoreSlim _lock = new(1, 1);
        #endregion

        #region Constructor
        public SmtpConnection(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }
        #endregion

        #region Methods
        public async Task SendAsync(MimeMessage message, CancellationToken cancellationToken = default)
        {
            await _lock.WaitAsync(cancellationToken);
            try
            {
                try
                {
                    await EnsureConnectedAsync(cancellationToken);
                    await _client.SendAsync(message, cancellationToken);
                }
                catch (Exception ex) when (ex is ServiceNotConnectedException or IOException or SmtpProtocolException)
                {
                    // Servers close idle connections, which is only detected on the next send: reconnect once and retry
                    await ResetAsync();
                    await EnsureConnectedAsync(cancellationToken);
                    await _client.SendAsync(message, cancellationToken);
                }
            }
            finally
            {
                _lock.Release();
            }
        }

        private async Task EnsureConnectedAsync(CancellationToken cancellationToken)
        {
            if (!_client.IsConnected)
                await _client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true, cancellationToken);

            if (!_client.IsAuthenticated)
                await _client.AuthenticateAsync(_emailSettings.FromEmail, _emailSettings.Password, cancellationToken);
        }

        private async Task ResetAsync()
        {
            try
            {
                if (_client.IsConnected)
                    await _client.DisconnectAsync(false);
            }
            catch
            {
                // The connection is already broken; the next connect starts clean
            }
        }

        public void Dispose()
        {
            try
            {
                if (_client.IsConnected)
                    _client.Disconnect(true);
            }
            catch
            {
                // Best effort on shutdown
            }
            _client.Dispose();
            _lock.Dispose();
        }
        #endregion
    }
}
