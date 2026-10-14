using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.SocialMedias.Commands.RequestModels
{
    public class SoftDeleteAndActivateSocialMediaCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
