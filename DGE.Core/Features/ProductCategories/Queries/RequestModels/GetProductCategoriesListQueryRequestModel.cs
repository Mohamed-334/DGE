using DGE.Core.Features.ProductCategories.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductCategories.Queries.RequestModels
{
    public class GetProductCategoriesListQueryRequestModel : IRequest<Response<List<ProductCategoryFullDataDto>>>
    {
    }
}
