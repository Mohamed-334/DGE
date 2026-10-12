using DGE.Core.Features.Certifications.Commands.RequestModels;
using DGE.Core.Features.Certifications.Queries.RequestModels;
using DGE.Domain.Meta;
using DGE.Presentation.Shared.BaseController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DGE.Presentation.Controllers
{
    [ApiController]
    [Authorize]
    public class CertificationController : BaseControllerApp
    {
        [HttpGet(Router.CertificationRouting.GetById)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = await _mediator.Send(new GetCertificationByIdQueryRequestModel()
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.CertificationRouting.GetList)]
        public async Task<IActionResult> GetList()
        {
            var response = await _mediator.Send(new GetCertificationsListQueryRequestModel());
            return Result(response);
        }
        [HttpGet(Router.CertificationRouting.GetDropDownList)]
        public async Task<IActionResult> GetDropDownList()
        {
            var response = await _mediator.Send(new GetCertificationsDropDownQueryRequestModel());
            return Result(response);
        }
        [AllowAnonymous]
        [HttpPost(Router.CertificationRouting.GetPaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromBody] GetCertificationsPaginatedListQueryRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPost(Router.CertificationRouting.Create)]
        public async Task<IActionResult> Create([FromForm] AddCertificationCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpPut(Router.CertificationRouting.Update)]
        public async Task<IActionResult> Update([FromForm] UpdateCertificationCommandRequestModel request)
        {
            var response = await _mediator.Send(request);
            return Result(response);
        }
        [HttpDelete(Router.CertificationRouting.HardDelete)]
        public async Task<IActionResult> HardDelete([FromRoute] int id)
        {
            var response = await _mediator.Send(new DeleteCertificationCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
        [HttpGet(Router.CertificationRouting.SoftDeleteAndActivate)]
        public async Task<IActionResult> SoftDeleteAndActivate([FromRoute] int id)
        {
            var response = await _mediator.Send(new SoftDeleteAndActivateCertificationCommandRequestModel
            {
                Id = id
            });
            return Result(response);
        }
    }
}
