using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductTypes.Commands.RequestModels
{
    public class DeleteProductTypeCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
