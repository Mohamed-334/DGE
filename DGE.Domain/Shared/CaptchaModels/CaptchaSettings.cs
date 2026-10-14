namespace DGE.Domain.Shared.CaptchaModels
{
    public class CaptchaSettings
    {
        // When false the token is not checked (local development without keys)
        public bool Enabled { get; set; }
        public string SecretKey { get; set; } = string.Empty;
        // Google reCAPTCHA by default; Cloudflare Turnstile uses the same contract at https://challenges.cloudflare.com/turnstile/v0/siteverify
        public string VerifyUrl { get; set; } = "https://www.google.com/recaptcha/api/siteverify";
        // Only applied when the provider returns a score (reCAPTCHA v3)
        public double MinimumScore { get; set; } = 0.5;
    }
}
