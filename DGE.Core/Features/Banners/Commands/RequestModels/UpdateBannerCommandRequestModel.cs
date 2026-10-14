using DGE.Core.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DGE.Core.Features.Banners.Commands.RequestModels
{
    public class UpdateBannerCommandRequestModel : BaseModel, IRequest<Response<string>>
    {
        public string? Title { get; set; }
        public string? TitleLocalization { get; set; }
        public string? Subtitle { get; set; }
        public string? SubtitleLocalization { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ButtonText { get; set; }
        public string? ButtonTextLocalization { get; set; }
        public string? Link { get; set; }
        // Left unchanged when not sent
        public int? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
    }
}
