using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.Minerals.Queries.RequestModels
{
    public class GetMineralsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
