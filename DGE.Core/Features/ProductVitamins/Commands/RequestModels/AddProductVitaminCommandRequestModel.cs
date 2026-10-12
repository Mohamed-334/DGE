using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductVitamins.Commands.RequestModels
{
    public class AddProductVitaminCommandRequestModel : IRequest<Response<string>>
    {
        public int ProductId { get; set; }
        public int VitaminId { get; set; }
    }
}
