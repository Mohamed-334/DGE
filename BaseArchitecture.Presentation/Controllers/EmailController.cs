using Asp.Versioning;
using BaseArchitecture.Core.Features.Email.Commands.RequestModels;
using BaseArchitecture.Domain.Meta;
using BaseArchitecture.Presentation.Shared.BaseController;
using BaseArchitecture.Presentation.Shared.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseArchitecture.Presentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class EmailController : BaseControllerApp
    {
        [HttpPost(Router.EmailRouting.SendEmail)]
        [EnableRateLimiting(RateLimitPolicies.EmailSend)]
        public async Task<IActionResult> SendEmail([FromQuery] SendEmailCommandRequestModel command, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(command, cancellationToken);
            return Result(response);
        }
    }
}
