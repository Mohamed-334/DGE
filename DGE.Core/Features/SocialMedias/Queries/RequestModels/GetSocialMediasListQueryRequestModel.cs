using DGE.Core.Features.SocialMedias.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.SocialMedias.Queries.RequestModels
{
    public class GetSocialMediasListQueryRequestModel : IRequest<Response<List<SocialMediaFullDataDto>>>
    {
    }
}
