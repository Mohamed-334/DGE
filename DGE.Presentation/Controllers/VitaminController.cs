using DGE.Core.Features.Vitamins.Commands.RequestModels;
using DGE.Core.Features.Vitamins.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class VitaminController : BaseControllerApp
    {
        [HttpGet(Router.VitaminRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetVitaminByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.VitaminRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetVitaminsListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.VitaminRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetVitaminsDropDownQueryRequestModel());
            return Result(response);
        }
        [HttpPost(Router.VitaminRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetVitaminsPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.VitaminRouting.Create)]
        public async Task<IActionResult> Create([FromForm] AddVitaminCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.VitaminRouting.Update)]
        public async Task<IActionResult> Update([FromForm] UpdateVitaminCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.VitaminRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteVitaminCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.VitaminRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateVitaminCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
