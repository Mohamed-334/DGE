using DGE.Core.Features.ProductTypes.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.ProductTypes.Queries.RequestModels
{
    public class GetProductTypesPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<ProductTypeFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? ProductId { get; set; }
        public int? TypeId { get; set; }
        public GetProductTypesPaginatedListQueryRequestModel(int pageNumber, int pageSize, int? productId, int? typeId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            ProductId = productId;
            TypeId = typeId;
        }
        public GetProductTypesPaginatedListQueryRequestModel() { }
    }
}
