using DGE.Core.Features.SiteSections.Commands.RequestModels;
using DGE.Core.Features.SiteSections.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class SiteSectionController : BaseControllerApp
    {
        [AllowAnonymous]
        [HttpGet(Router.SiteSectionRouting.GetByKey)]
        public async Task<IActionResult> GetByKey([FromRoute] string key)
        {
            var response = await _mediator.Send(new GetSiteSectionByKeyQueryRequestModel()
            {
                Key = key
            });
            return Result(response);
        }
        [HttpPut(Router.SiteSectionRouting.Update)]
        public async Task<IActionResult> Update([FromForm] UpdateSiteSectionCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
    }
}
