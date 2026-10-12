using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductVitamins.Commands.RequestModels
{
    public class UpdateProductVitaminCommandRequestModel : BaseModel, IRequest<Response<string>>
    {
        public int ProductId { get; set; }
        public int VitaminId { get; set; }
    }
}
