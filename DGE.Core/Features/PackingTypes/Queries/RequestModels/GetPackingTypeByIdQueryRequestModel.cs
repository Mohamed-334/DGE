using DGE.Core.Features.PackingTypes.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.PackingTypes.Queries.RequestModels
{
    public class GetPackingTypeByIdQueryRequestModel : IRequest<Response<PackingTypeFullDataDto>>
    {
        public int Id { get; set; }
    }
}
