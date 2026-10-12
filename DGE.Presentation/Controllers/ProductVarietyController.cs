using DGE.Core.Features.ProductVarieties.Commands.RequestModels;
using DGE.Core.Features.ProductVarieties.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class ProductVarietyController : BaseControllerApp
    {
        [HttpGet(Router.ProductVarietyRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetProductVarietyByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ProductVarietyRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetProductVarietiesListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.ProductVarietyRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetProductVarietiesDropDownQueryRequestModel());
            return Result(response);
        }
        [HttpPost(Router.ProductVarietyRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetProductVarietiesPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.ProductVarietyRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddProductVarietyCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.ProductVarietyRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateProductVarietyCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.ProductVarietyRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteProductVarietyCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ProductVarietyRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateProductVarietyCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
