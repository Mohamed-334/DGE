using Asp.Versioning;
using DGE.Core.Features.ApplicationUser.Commands.RequestModels;
using DGE.Core.Features.ApplicationUser.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class UserController : BaseControllerApp
    {
        [HttpGet(Router.UserRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUserByIdQueryRequestModel()
            {
                UserId = id
            }, cancellationToken);
            return Result(response);
        }
        [HttpGet(Router.UserRouting.GetList)]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUsersListQueryRequestModel(), cancellationToken);
            return Result(response);
        }
        [HttpGet(Router.UserRouting.GetUserRoles)]
        public async Task<IActionResult> GetUserRoles([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUserRolesQueryRequestModel()
            {
                UserId = id
            }, cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.UserRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetUsersPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPut(Router.UserRouting.Update)]
        public async Task<IActionResult> Update([FromForm] UpdateUserCommandRequestQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpDelete(Router.UserRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteUserCommandRequestQuery
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
        [HttpPatch(Router.UserRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateUserCommandRequestQuery
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
    }
}
