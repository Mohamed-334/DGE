using Asp.Versioning;
using DGE.Core.Features.Authentication.Commands.RequestModels;
using DGE.Core.Shared.Models;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using DGE.Presentation.Shared.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class AuthenticationController : BaseControllerApp
    {
        [HttpGet(Router.AuthenticationRouting.GoogleAuthenticationRequest)]
        [EnableRateLimiting(RateLimitPolicies.OAuth)]
        public async Task<IActionResult> GoogleAuthenticationRequest(CancellationToken cancellationToken)
        {
            var redirectUrl = Url.Action(nameof(ExternalAuthentication), "Authentication", new { provider = "Google" });
            var response = await _mediator.Send(new GoogleAuthenticationRequestCommandRequestModel
            {
                RedirectUrl = redirectUrl
            }, cancellationToken);
            return Challenge(response?.Data ?? new AuthenticationProperties(), GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet(Router.AuthenticationRouting.LinkedInAuthenticationRequest)]
        [EnableRateLimiting(RateLimitPolicies.OAuth)]
        public async Task<IActionResult> LinkedInAuthenticationRequest(CancellationToken cancellationToken)
        {
            var redirectUrl = Url.Action(nameof(ExternalAuthentication), "Authentication", new { provider = "LinkedIn" });
            var response = await _mediator.Send(new LinkedInAuthenticationRequestCommandRequestModel
            {
                RedirectUrl = redirectUrl
            }, cancellationToken);
            return Challenge(response.Data ?? new AuthenticationProperties(), "LinkedIn");
        }
        [HttpGet(Router.AuthenticationRouting.ExternalAuthentication)]
        [EnableRateLimiting(RateLimitPolicies.OAuth)]
        public async Task<IActionResult> ExternalAuthentication([FromQuery] string provider, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new ExternalAuthenticationCommandRequestModel(), cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.AuthenticationRouting.SignUp)]
        [IgnoreAntiforgeryToken]
        [EnableRateLimiting(RateLimitPolicies.SignUp)]
        public async Task<IActionResult> SignUp([FromForm] SignUpCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        [IgnoreAntiforgeryToken]
        [EnableRateLimiting(RateLimitPolicies.Auth)]
        public async Task<IActionResult> SignIn([FromBody] SignInCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.AuthenticationRouting.ResendOtp)]
        [IgnoreAntiforgeryToken]
        [EnableRateLimiting(RateLimitPolicies.OtpResend)]
        public async Task<IActionResult> ResendOtp([FromBody] ResendOtpCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.AuthenticationRouting.VerifyRegistrationOtp)]
        [IgnoreAntiforgeryToken]
        [EnableRateLimiting(RateLimitPolicies.Auth)]
        public async Task<IActionResult> VerifyRegistrationOtp([FromBody] OtpVerificationCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.AuthenticationRouting.VerifyResetPasswordOtp)]
        [IgnoreAntiforgeryToken]
        [EnableRateLimiting(RateLimitPolicies.Auth)]
        public async Task<IActionResult> VerifyResetPasswordOtp([FromBody] ResetPasswordOtpVerificationCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPut(Router.AuthenticationRouting.ChangePassword)]
        [EnableRateLimiting(RateLimitPolicies.PasswordChange)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.AuthenticationRouting.ResetPasswordRequest)]
        [IgnoreAntiforgeryToken]
        [EnableRateLimiting(RateLimitPolicies.PasswordResetRequest)]
        public async Task<IActionResult> ResetPasswordRequest([FromBody] ResetPasswordRequestCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.AuthenticationRouting.ResetPassword)]
        [IgnoreAntiforgeryToken]
        [EnableRateLimiting(RateLimitPolicies.PasswordReset)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpGet(Router.AuthenticationRouting.CsrfToken)]
        public IActionResult CsrfToken([FromServices] IAntiforgery antiforgery)
        {
            var tokens = antiforgery.GetAndStoreTokens(HttpContext);
            return Result(new Response<string>(tokens.RequestToken) { StatusCode = HttpStatusCode.OK });
        }
        [HttpPost(Router.AuthenticationRouting.Logout)]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new LogoutCommandRequestModel(), cancellationToken);
            return Result(response);
        }
    }
}
