using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.ProductImages.Queries.RequestModels
{
    public class GetProductImagesDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
