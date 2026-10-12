using DGE.Core.Features.ProductVitamins.Commands.RequestModels;
using DGE.Core.Features.ProductVitamins.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class ProductVitaminController : BaseControllerApp
    {
        [HttpGet(Router.ProductVitaminRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetProductVitaminByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ProductVitaminRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetProductVitaminsListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.ProductVitaminRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetProductVitaminsDropDownQueryRequestModel());
            return Result(response);
        }
        [HttpPost(Router.ProductVitaminRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetProductVitaminsPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.ProductVitaminRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddProductVitaminCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.ProductVitaminRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateProductVitaminCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.ProductVitaminRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteProductVitaminCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ProductVitaminRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateProductVitaminCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
