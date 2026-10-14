using DGE.Domain.Shared.CaptchaModels;
using DGE.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace DGE.Service.Service
{
    public class CaptchaService : ICaptchaService
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly CaptchaSettings _captchaSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<CaptchaService> _logger;
        #endregion

        #region Constructor
        public CaptchaService(HttpClient httpClient, CaptchaSettings captchaSettings, IHttpContextAccessor httpContextAccessor, ILogger<CaptchaService> logger)
        {
            _httpClient = httpClient;
            _captchaSettings = captchaSettings;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }
        #endregion

        #region Methods
        public async Task<bool> VerifyAsync(string? token, CancellationToken cancellationToken = default)
        {
            if (!_captchaSettings.Enabled)
                return true;

            // Enabled without a secret is a misconfiguration: fail closed instead of silently accepting everything
            if (string.IsNullOrWhiteSpace(_captchaSettings.SecretKey))
            {
                _logger.LogError("CAPTCHA is enabled but no secret key is configured");
                return false;
            }

            if (string.IsNullOrWhiteSpace(token))
                return false;

            var form = new Dictionary<string, string>
            {
                ["secret"] = _captchaSettings.SecretKey,
                ["response"] = token
            };
            var remoteIp = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            if (!string.IsNullOrEmpty(remoteIp))
                form["remoteip"] = remoteIp;

            try
            {
                using var response = await _httpClient.PostAsync(_captchaSettings.VerifyUrl, new FormUrlEncodedContent(form), cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("CAPTCHA verification returned {StatusCode}", response.StatusCode);
                    return false;
                }

                var result = await response.Content.ReadFromJsonAsync<CaptchaVerifyResponse>(cancellationToken);
                if (result == null || !result.Success)
                    return false;
                return result.Score == null || result.Score >= _captchaSettings.MinimumScore;
            }
            catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or System.Text.Json.JsonException)
            {
                _logger.LogError(ex, "CAPTCHA verification request failed");
                return false;
            }
        }
        #endregion

        private class CaptchaVerifyResponse
        {
            [JsonPropertyName("success")]
            public bool Success { get; set; }

            [JsonPropertyName("score")]
            public double? Score { get; set; }
        }
    }
}
