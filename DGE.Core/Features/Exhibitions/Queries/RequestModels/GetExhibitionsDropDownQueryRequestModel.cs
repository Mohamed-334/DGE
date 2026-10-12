using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.Exhibitions.Queries.RequestModels
{
    public class GetExhibitionsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
