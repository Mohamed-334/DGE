using DGE.Core.Features.SocialMedias.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.SocialMedias.Queries.RequestModels
{
    public class GetSocialMediasPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<SocialMediaFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool? IsActive { get; set; }
        public GetSocialMediasPaginatedListQueryRequestModel(int pageNumber, int pageSize, bool? isActive = null)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            IsActive = isActive;
        }
        public GetSocialMediasPaginatedListQueryRequestModel() { }
    }
}
