using DGE.Core.Features.Vitamins.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Vitamins.Queries.RequestModels
{
    public class GetVitaminsListQueryRequestModel : IRequest<Response<List<VitaminFullDataDto>>>
    {
    }
}
