using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductVarieties.Commands.RequestModels
{
    public class SoftDeleteAndActivateProductVarietyCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
