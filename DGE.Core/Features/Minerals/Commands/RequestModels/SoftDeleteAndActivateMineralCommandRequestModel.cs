using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Minerals.Commands.RequestModels
{
    public class SoftDeleteAndActivateMineralCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
