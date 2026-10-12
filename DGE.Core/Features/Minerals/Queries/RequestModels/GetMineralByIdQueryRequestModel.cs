using DGE.Core.Features.Minerals.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Minerals.Queries.RequestModels
{
    public class GetMineralByIdQueryRequestModel : IRequest<Response<MineralFullDataDto>>
    {
        public int Id { get; set; }
    }
}
