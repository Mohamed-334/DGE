using DGE.Core.Features.ProductCategories.Commands.RequestModels;
using DGE.Core.Features.ProductCategories.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class ProductCategoryController : BaseControllerApp
    {
        [HttpGet(Router.ProductCategoryRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetProductCategoryByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ProductCategoryRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetProductCategoriesListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.ProductCategoryRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetProductCategoriesDropDownQueryRequestModel());
            return Result(response);
        }
        [HttpPost(Router.ProductCategoryRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetProductCategoriesPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.ProductCategoryRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddProductCategoryCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.ProductCategoryRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateProductCategoryCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.ProductCategoryRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteProductCategoryCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ProductCategoryRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateProductCategoryCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
