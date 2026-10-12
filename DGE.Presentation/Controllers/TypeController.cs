using DGE.Core.Features.Types.Commands.RequestModels;
using DGE.Core.Features.Types.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class TypeController : BaseControllerApp
    {
        [HttpGet(Router.TypeRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetTypeByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.TypeRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetTypesListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.TypeRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetTypesDropDownQueryRequestModel());
            return Result(response);
        }
        [HttpPost(Router.TypeRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetTypesPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.TypeRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddTypeCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.TypeRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateTypeCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.TypeRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteTypeCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.TypeRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateTypeCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
