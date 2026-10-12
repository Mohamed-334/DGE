using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductPackings.Commands.RequestModels
{
    public class UpdateProductPackingCommandRequestModel : BaseModel, IRequest<Response<string>>
    {
        public int? ProductId { get; set; }
        public int? PackingTypeId { get; set; }
    }
}
