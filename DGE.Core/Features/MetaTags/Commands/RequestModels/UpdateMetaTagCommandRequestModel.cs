using DGE.Core.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DGE.Core.Features.MetaTags.Commands.RequestModels
{
    public class UpdateMetaTagCommandRequestModel : IRequest<Response<string>>
    {
        // home | about | products | certifications | exhibitions | export | quality | contact
        public string? Page { get; set; }
        public string? Title { get; set; }
        public string? TitleLocalization { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public string? Keywords { get; set; }
        // Keeps the current image when not sent
        public IFormFile? OgImageFile { get; set; }
    }
}
