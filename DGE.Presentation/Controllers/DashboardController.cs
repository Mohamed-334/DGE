using DGE.Core.Features.Dashboard.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class DashboardController : BaseControllerApp
    {
        [HttpGet(Router.DashboardRouting.Stats)]
        public async Task<IActionResult> Stats()
        {
            var response = await _mediator.Send(new GetDashboardStatsQueryRequestModel());
            return Result(response);
        }
    }
}
