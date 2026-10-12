using DGE.Core.Features.Exhibitions.Commands.RequestModels;
using DGE.Core.Features.Exhibitions.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class ExhibitionController : BaseControllerApp
    {
        [HttpGet(Router.ExhibitionRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetExhibitionByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ExhibitionRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetExhibitionsListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.ExhibitionRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetExhibitionsDropDownQueryRequestModel());
            return Result(response);
        }
        [AllowAnonymous]
        [HttpPost(Router.ExhibitionRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetExhibitionsPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.ExhibitionRouting.Create)]
        public async Task<IActionResult> Create([FromForm] AddExhibitionCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.ExhibitionRouting.Update)]
        public async Task<IActionResult> Update([FromForm] UpdateExhibitionCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.ExhibitionRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteExhibitionCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ExhibitionRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateExhibitionCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
