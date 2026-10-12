using DGE.Core.Features.Exhibitions.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Exhibitions.Queries.RequestModels
{
    public class GetExhibitionByIdQueryRequestModel : IRequest<Response<ExhibitionFullDataDto>>
    {
        public int Id { get; set; }
    }
}
