using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductVitamins.Commands.RequestModels
{
    public class DeleteProductVitaminCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
