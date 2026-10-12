using DGE.Core.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DGE.Core.Features.Vitamins.Commands.RequestModels
{
    public class UpdateVitaminCommandRequestModel : BaseModelWithName, IRequest<Response<string>>
    {
        public IFormFile? VitaminUrlFile { get; set; }
    }
}
