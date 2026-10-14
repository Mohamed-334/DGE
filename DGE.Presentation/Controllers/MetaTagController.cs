using DGE.Core.Features.MetaTags.Commands.RequestModels;
using DGE.Core.Features.MetaTags.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class MetaTagController : BaseControllerApp
    {
        [AllowAnonymous]
        [HttpGet(Router.MetaTagRouting.GetByPage)]
        public async Task<IActionResult> GetByPage([FromRoute] string page)
        {
            var response = await _mediator.Send(new GetMetaTagByPageQueryRequestModel()
            {
                Page = page
            });
            return Result(response);
        }
        [HttpPut(Router.MetaTagRouting.Update)]
        public async Task<IActionResult> Update([FromForm] UpdateMetaTagCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
    }
}
