using DGE.Core.Features.PackingTypes.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.PackingTypes.Queries.RequestModels
{
    public class GetPackingTypesListQueryRequestModel : IRequest<Response<List<PackingTypeFullDataDto>>>
    {
    }
}
