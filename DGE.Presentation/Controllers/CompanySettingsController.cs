using DGE.Core.Features.CompanySettings.Commands.RequestModels;
using DGE.Core.Features.CompanySettings.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class CompanySettingsController : BaseControllerApp
    {
        [AllowAnonymous]
        [HttpGet(Router.CompanySettingsRouting.Get)]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetCompanySettingsQueryRequestModel());
            return Result(response);
        }
        [HttpPut(Router.CompanySettingsRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateCompanySettingsCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
    }
}
