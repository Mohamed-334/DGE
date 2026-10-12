using Asp.Versioning;
using DGE.Core.Features.RolePermissions.Commands.RequestModels;
using DGE.Core.Features.RolePermissions.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class RolePermissionController : BaseControllerApp
    {
        [HttpGet(Router.RolePermissionRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetRolePermissionByIdQueryRequestModel()
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
        [HttpGet(Router.RolePermissionRouting.GetList)]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetRolePermissionsListQueryRequestModel(), cancellationToken);
            return Result(response);
        }
        [HttpGet(Router.RolePermissionRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetRolePermissionsDropDownQueryRequestModel(), cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.RolePermissionRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetRolePermissionsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.RolePermissionRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddRolePermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPut(Router.RolePermissionRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateRolePermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpDelete(Router.RolePermissionRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteRolePermissionCommandRequestModel
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
        [HttpPatch(Router.RolePermissionRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateRolePermissionCommandRequestModel
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
    }
}
