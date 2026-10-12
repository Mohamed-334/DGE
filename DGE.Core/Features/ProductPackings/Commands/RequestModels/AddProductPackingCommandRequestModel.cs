using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductPackings.Commands.RequestModels
{
    public class AddProductPackingCommandRequestModel : IRequest<Response<string>>
    {
        public int? ProductId { get; set; }
        public int? PackingTypeId { get; set; }
    }
}
