using DGE.Core.Features.Minerals.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Minerals.Queries.RequestModels
{
    public class GetMineralsListQueryRequestModel : IRequest<Response<List<MineralFullDataDto>>>
    {
    }
}
