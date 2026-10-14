using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.SocialMedias.Commands.RequestModels
{
    public class UpdateSocialMediaCommandRequestModel : BaseModel, IRequest<Response<string>>
    {
        public string? Platform { get; set; }
        public string? Url { get; set; }
        // Left unchanged when not sent
        public int? DisplayOrder { get; set; }
        public bool? IsActive { get; set; }
    }
}
