using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Vitamins.Commands.RequestModels
{
    public class SoftDeleteAndActivateVitaminCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
