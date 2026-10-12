using DGE.Core.Features.ProductHarvestMonths.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.ProductHarvestMonths.Queries.RequestModels
{
    public class GetProductHarvestMonthsPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<ProductHarvestMonthFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? ProductId { get; set; }
        public int? MonthStatusCode { get; set; }
        public GetProductHarvestMonthsPaginatedListQueryRequestModel(int pageNumber, int pageSize, int? productId, int? monthStatusCode)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            ProductId = productId;
            MonthStatusCode = monthStatusCode;
        }
        public GetProductHarvestMonthsPaginatedListQueryRequestModel() { }
    }
}
