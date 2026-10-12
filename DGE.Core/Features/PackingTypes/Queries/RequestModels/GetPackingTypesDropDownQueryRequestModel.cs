using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.PackingTypes.Queries.RequestModels
{
    public class GetPackingTypesDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
