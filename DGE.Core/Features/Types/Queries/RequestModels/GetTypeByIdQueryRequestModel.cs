using DGE.Core.Features.Types.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Types.Queries.RequestModels
{
    public class GetTypeByIdQueryRequestModel : IRequest<Response<TypeFullDataDto>>
    {
        public int Id { get; set; }
    }
}
