using DGE.Core.Features.SocialMedias.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.SocialMedias.Queries.RequestModels
{
    public class GetSocialMediaByIdQueryRequestModel : IRequest<Response<SocialMediaFullDataDto>>
    {
        public int Id { get; set; }
    }
}
