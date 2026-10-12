using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductVarieties.Commands.RequestModels
{
    public class UpdateProductVarietyCommandRequestModel : BaseModelWithName, IRequest<Response<string>>
    {
        public int ProductId { get; set; }
    }
}
