using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.Types.Queries.RequestModels
{
    public class GetTypesDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
