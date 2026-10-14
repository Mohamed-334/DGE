using DGE.Core.Features.Banners.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Banners.Queries.RequestModels
{
    public class GetBannerByIdQueryRequestModel : IRequest<Response<BannerFullDataDto>>
    {
        public int Id { get; set; }
    }
}
