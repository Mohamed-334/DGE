using Asp.Versioning;
using DGE.Core.Features.Email.Commands.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using DGE.Presentation.Shared.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
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
