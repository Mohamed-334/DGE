using DGE.Core.Features.ProductPackings.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.ProductPackings.Queries.RequestModels
{
    public class GetProductPackingsPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<ProductPackingFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? ProductId { get; set; }
        public int? PackingTypeId { get; set; }
        public GetProductPackingsPaginatedListQueryRequestModel(int pageNumber, int pageSize, int? productId, int? packingTypeId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            ProductId = productId;
            PackingTypeId = packingTypeId;
        }
        public GetProductPackingsPaginatedListQueryRequestModel() { }
    }
}
