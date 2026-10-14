using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.SocialMedias.Queries.RequestModels
{
    public class GetSocialMediasDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
