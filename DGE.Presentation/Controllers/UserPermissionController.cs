using Asp.Versioning;
using DGE.Core.Features.UserPermissions.Commands.RequestModels;
using DGE.Core.Features.UserPermissions.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class UserPermissionController : BaseControllerApp
    {
        [HttpGet(Router.UserPermissionRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUserPermissionByIdQueryRequestModel()
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
        [HttpGet(Router.UserPermissionRouting.GetList)]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUserPermissionsListQueryRequestModel(), cancellationToken);
            return Result(response);
        }
        [HttpGet(Router.UserPermissionRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetUserPermissionsDropDownQueryRequestModel(), cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.UserPermissionRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetUserPermissionsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.UserPermissionRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserPermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPut(Router.UserPermissionRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateUserPermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpDelete(Router.UserPermissionRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteUserPermissionCommandRequestModel
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
        [HttpPatch(Router.UserPermissionRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateUserPermissionCommandRequestModel
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
    }
}
