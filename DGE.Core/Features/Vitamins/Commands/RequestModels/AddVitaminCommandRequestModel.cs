using DGE.Core.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DGE.Core.Features.Vitamins.Commands.RequestModels
{
    public class AddVitaminCommandRequestModel : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
        public IFormFile? VitaminUrlFile { get; set; }
    }
}
