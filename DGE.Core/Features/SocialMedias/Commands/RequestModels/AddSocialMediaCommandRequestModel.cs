using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.SocialMedias.Commands.RequestModels
{
    public class AddSocialMediaCommandRequestModel : IRequest<Response<string>>
    {
        public string? Platform { get; set; }
        public string? Url { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
