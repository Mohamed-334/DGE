using DGE.Core.Shared.Models;
using DGE.Domain.Shared.BaseEntity.Implementations;
using MediatR;

namespace DGE.Core.Features.Vitamins.Queries.RequestModels
{
    public class GetVitaminsDropDownQueryRequestModel : IRequest<Response<List<DropDown>>>
    {
    }
}
