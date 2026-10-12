using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Products.Commands.RequestModels
{
    public class DeleteProductCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
