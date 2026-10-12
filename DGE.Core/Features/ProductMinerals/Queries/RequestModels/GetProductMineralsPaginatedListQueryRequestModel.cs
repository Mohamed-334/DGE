using DGE.Core.Features.ProductMinerals.Dto;
using DGE.Core.Shared.Models;
using DGE.Service.Shared.PaginatedList;
using MediatR;

namespace DGE.Core.Features.ProductMinerals.Queries.RequestModels
{
    public class GetProductMineralsPaginatedListQueryRequestModel : IRequest<Response<PaginatedList<ProductMineralFullDataDto>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? ProductId { get; set; }
        public int? MineralId { get; set; }
        public GetProductMineralsPaginatedListQueryRequestModel(int pageNumber, int pageSize, int? productId, int? mineralId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            ProductId = productId;
            MineralId = mineralId;
        }
        public GetProductMineralsPaginatedListQueryRequestModel() { }
    }
}
