using DGE.Core.Features.SocialMedias.Commands.RequestModels;
using DGE.Core.Features.SocialMedias.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class SocialMediaController : BaseControllerApp
    {
        [AllowAnonymous]
        [HttpGet(Router.SocialMediaRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetSocialMediaByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [AllowAnonymous]
        [HttpGet(Router.SocialMediaRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetSocialMediasListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.SocialMediaRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetSocialMediasDropDownQueryRequestModel());
            return Result(response);
        }
        [AllowAnonymous]
        [HttpPost(Router.SocialMediaRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetSocialMediasPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.SocialMediaRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddSocialMediaCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.SocialMediaRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateSocialMediaCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.SocialMediaRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteSocialMediaCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.SocialMediaRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateSocialMediaCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
