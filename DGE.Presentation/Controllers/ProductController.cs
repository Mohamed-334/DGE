using DGE.Core.Features.Products.Commands.RequestModels;
using DGE.Core.Features.Products.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class ProductController : BaseControllerApp
    {
        [HttpGet(Router.ProductRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetProductByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ProductRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetProductsListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.ProductRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetProductsDropDownQueryRequestModel());
            return Result(response);
        }
        [HttpPost(Router.ProductRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetProductsPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.ProductRouting.Create)]
        public async Task<IActionResult> Create([FromForm] AddProductCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.ProductRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.ProductRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteProductCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ProductRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateProductCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
