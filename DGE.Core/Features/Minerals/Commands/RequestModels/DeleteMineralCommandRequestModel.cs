using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Minerals.Commands.RequestModels
{
    public class DeleteMineralCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
