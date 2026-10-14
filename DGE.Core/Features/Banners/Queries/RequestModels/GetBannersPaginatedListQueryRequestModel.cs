using DGE.Core.Features.Banners.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.Banners.Queries.RequestModels
{
    public class GetBannersPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<BannerFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool? IsActive { get; set; }
        public GetBannersPaginatedListQueryRequestModel(int pageNumber, int pageSize, bool? isActive = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            IsActive = isActive;
        }
        public GetBannersPaginatedListQueryRequestModel() { }
    }
}
