using DGE.Core.Features.ProductCategories.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductCategories.Queries.RequestModels
{
    public class GetProductCategoryByIdQueryRequestModel : IRequest<Response<ProductCategoryFullDataDto>>
    {
        public int Id { get; set; }
    }
}
