using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductVarieties.Commands.RequestModels
{
    public class DeleteProductVarietyCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
