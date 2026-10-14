using DGE.Core.Features.Banners.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Banners.Queries.RequestModels
{
    public class GetBannersListQueryRequestModel : IRequest<Response<List<BannerFullDataDto>>>
    {
    }
}
