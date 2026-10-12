using Asp.Versioning;
using DGE.Core.Features.Permissions.Commands.RequestModels;
using DGE.Core.Features.Permissions.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    public class PermissionController : BaseControllerApp
    {
        [HttpGet(Router.PermissionRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetPermissionByIdQueryRequestModel()
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
        [HttpGet(Router.PermissionRouting.GetList)]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetPermissionsListQueryRequestModel(), cancellationToken);
            return Result(response);
        }
        [HttpGet(Router.PermissionRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetPermissionsDropDownQueryRequestModel(), cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.PermissionRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetPermissionsPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.PermissionRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddPermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPut(Router.PermissionRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdatePermissionCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpDelete(Router.PermissionRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeletePermissionCommandRequestModel
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
        [HttpPatch(Router.PermissionRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivatePermissionCommandRequestModel
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
    }
}
