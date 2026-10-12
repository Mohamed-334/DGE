using Asp.Versioning;
using DGE.Core.Features.Notifications.Commands.RequestModels;
using DGE.Core.Features.Notifications.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using DGE.Presentation.Shared.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Presentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class NotificationController : BaseControllerApp
    {
        [HttpGet(Router.NotificationRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetNotificationByIdQueryRequestModel()
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
        [HttpGet(Router.NotificationRouting.GetList)]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetNotificationsListQueryRequestModel(), cancellationToken);
            return Result(response);
        }
        [HttpGet(Router.NotificationRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetNotificationsDropDownQueryRequestModel(), cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.NotificationRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetNotificationsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.NotificationRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddNotificationCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.NotificationRouting.Send)]
        [EnableRateLimiting(RateLimitPolicies.NotificationSend)]
        public async Task<IActionResult> Send([FromBody] SendNotificationCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPut(Router.NotificationRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateNotificationCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpDelete(Router.NotificationRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteNotificationCommandRequestModel
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
        [HttpPatch(Router.NotificationRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateNotificationCommandRequestModel
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
    }
}
