using DGE.Core.Features.Banners.Commands.RequestModels;
using DGE.Core.Features.Banners.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class BannerController : BaseControllerApp
    {
        [AllowAnonymous]
        [HttpGet(Router.BannerRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetBannerByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [AllowAnonymous]
        [HttpGet(Router.BannerRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetBannersListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.BannerRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetBannersDropDownQueryRequestModel());
            return Result(response);
        }
        [AllowAnonymous]
        [HttpPost(Router.BannerRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetBannersPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.BannerRouting.Create)]
        public async Task<IActionResult> Create([FromForm] AddBannerCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.BannerRouting.Update)]
        public async Task<IActionResult> Update([FromForm] UpdateBannerCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.BannerRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteBannerCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.BannerRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateBannerCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
