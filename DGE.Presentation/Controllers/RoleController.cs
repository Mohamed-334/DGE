using Asp.Versioning;
using DGE.Core.Features.Roles.Commands.RequestModels;
using DGE.Core.Features.Roles.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class RoleController : BaseControllerApp
    {
        [HttpGet(Router.RoleRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetRoleByIdQueryRequestModel()
            {
                RoleId = id
            }, cancellationToken);
            return Result(response);
        }
        [HttpGet(Router.RoleRouting.GetList)]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetRolesListQueryRequestModel(), cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.RoleRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetRolesPaginatedListQueryRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPost(Router.RoleRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddRoleCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpPut(Router.RoleRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateRoleCommandRequestModel request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Result(response);
        }
        [HttpDelete(Router.RoleRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new DeleteRoleCommandRequestModel
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
        [HttpPatch(Router.RoleRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateRoleCommandRequestQuery
            {
                Id = id
            }, cancellationToken);
            return Result(response);
        }
    }
}
