using DGE.Core.Features.Vitamins.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Vitamins.Queries.RequestModels
{
    public class GetVitaminByIdQueryRequestModel : IRequest<Response<VitaminFullDataDto>>
    {
        public int Id { get; set; }
    }
}
