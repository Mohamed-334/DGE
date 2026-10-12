using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductPackings.Commands.RequestModels
{
    public class DeleteProductPackingCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
