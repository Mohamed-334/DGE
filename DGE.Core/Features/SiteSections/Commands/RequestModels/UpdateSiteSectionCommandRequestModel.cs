using DGE.Core.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DGE.Core.Features.SiteSections.Commands.RequestModels
{
    public class UpdateSiteSectionCommandRequestModel : IRequest<Response<string>>
    {
        // welcome | about | quality | export
        public string? Key { get; set; }
        public string? Title { get; set; }
        public string? TitleLocalization { get; set; }
        public string? Details { get; set; }
        public string? DetailsLocalization { get; set; }
        // Keeps the current image when not sent
        public IFormFile? ImageFile { get; set; }
        // When sent, replaces all the current items in this order; when not sent the items stay as they are
        public List<SiteSectionItemRequestModel>? Items { get; set; }
        // Removes every item (an empty list cannot be told apart from a missing one in multipart)
        public bool ClearItems { get; set; }
    }

    public class SiteSectionItemRequestModel
    {
        public string? Title { get; set; }
        public string? TitleLocalization { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public string? Icon { get; set; }
    }
}
