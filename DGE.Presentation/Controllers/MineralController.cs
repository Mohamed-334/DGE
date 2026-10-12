using DGE.Core.Features.Minerals.Commands.RequestModels;
using DGE.Core.Features.Minerals.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class MineralController : BaseControllerApp
    {
        [HttpGet(Router.MineralRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetMineralByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.MineralRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetMineralsListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.MineralRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetMineralsDropDownQueryRequestModel());
            return Result(response);
        }
        [HttpPost(Router.MineralRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetMineralsPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.MineralRouting.Create)]
        public async Task<IActionResult> Create([FromForm] AddMineralCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.MineralRouting.Update)]
        public async Task<IActionResult> Update([FromForm] UpdateMineralCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.MineralRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteMineralCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.MineralRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateMineralCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
