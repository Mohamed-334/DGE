using DGE.Core.Features.ContactUsMessages.Commands.RequestModels;
using DGE.Core.Features.ContactUsMessages.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class ContactUsController : BaseControllerApp
    {
        [HttpGet(Router.ContactUsRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetContactUsByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ContactUsRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetContactUsMessagesListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.ContactUsRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetContactUsMessagesDropDownQueryRequestModel());
            return Result(response);
        }
        [HttpPost(Router.ContactUsRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetContactUsMessagesPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [AllowAnonymous]
        [HttpPost(Router.ContactUsRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddContactUsCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.ContactUsRouting.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateContactUsCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.ContactUsRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteContactUsCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.ContactUsRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateContactUsCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
