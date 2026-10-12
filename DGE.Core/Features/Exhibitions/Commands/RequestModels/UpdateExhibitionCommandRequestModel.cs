using DGE.Core.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DGE.Core.Features.Exhibitions.Commands.RequestModels
{
    public class UpdateExhibitionCommandRequestModel : BaseModelWithName, IRequest<Response<string>>
    {
        public IFormFile? ExhibitionUrlFile { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public string? Location { get; set; }
        public string? Year { get; set; }
    }
}
