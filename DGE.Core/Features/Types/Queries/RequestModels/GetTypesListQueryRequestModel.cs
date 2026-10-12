using DGE.Core.Features.Types.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Types.Queries.RequestModels
{
    public class GetTypesListQueryRequestModel : IRequest<Response<List<TypeFullDataDto>>>
    {
    }
}
