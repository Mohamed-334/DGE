using DGE.Core.Features.PackingTypes.Commands.RequestModels;
using DGE.Core.Features.PackingTypes.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class PackingTypeController : BaseControllerApp
    {
        [HttpGet(Router.PackingTypeRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetPackingTypeByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.PackingTypeRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetPackingTypesListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.PackingTypeRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetPackingTypesDropDownQueryRequestModel());
            return Result(response);
        }
        [HttpPost(Router.PackingTypeRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetPackingTypesPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.PackingTypeRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddPackingTypeCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.PackingTypeRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdatePackingTypeCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.PackingTypeRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeletePackingTypeCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.PackingTypeRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivatePackingTypeCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
