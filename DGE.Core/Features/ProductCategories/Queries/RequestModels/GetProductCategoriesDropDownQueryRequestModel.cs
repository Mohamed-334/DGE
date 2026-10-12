using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.ProductCategories.Queries.RequestModels
{
    public class GetProductCategoriesDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
