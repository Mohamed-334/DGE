using DGE.Core.Features.ProductMinerals.Commands.RequestModels;
using DGE.Core.Features.ProductMinerals.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class ProductMineralController : BaseControllerApp
    {
        [HttpGet(Router.ProductMineralRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetProductMineralByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ProductMineralRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetProductMineralsListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.ProductMineralRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetProductMineralsDropDownQueryRequestModel());
            return Result(response);
        }
        [HttpPost(Router.ProductMineralRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetProductMineralsPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.ProductMineralRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddProductMineralCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.ProductMineralRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateProductMineralCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.ProductMineralRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteProductMineralCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ProductMineralRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateProductMineralCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
