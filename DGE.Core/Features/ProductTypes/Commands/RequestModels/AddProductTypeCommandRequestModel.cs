using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductTypes.Commands.RequestModels
{
    public class AddProductTypeCommandRequestModel : IRequest<Response<string>>
    {
        public int ProductId { get; set; }
        public int TypeId { get; set; }
    }
}
