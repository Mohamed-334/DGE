using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Exhibitions.Commands.RequestModels
{
    public class DeleteExhibitionCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
