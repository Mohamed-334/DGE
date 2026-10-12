using DGE.Core.Features.ProductHarvestMonths.Commands.RequestModels;
using DGE.Core.Features.ProductHarvestMonths.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class ProductHarvestMonthController : BaseControllerApp
    {
        [HttpGet(Router.ProductHarvestMonthRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetProductHarvestMonthByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ProductHarvestMonthRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetProductHarvestMonthsListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.ProductHarvestMonthRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetProductHarvestMonthsDropDownQueryRequestModel());
            return Result(response);
        }
        [HttpPost(Router.ProductHarvestMonthRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetProductHarvestMonthsPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.ProductHarvestMonthRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddProductHarvestMonthCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.ProductHarvestMonthRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateProductHarvestMonthCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.ProductHarvestMonthRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteProductHarvestMonthCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ProductHarvestMonthRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateProductHarvestMonthCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
