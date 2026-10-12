using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Products.Commands.RequestModels
{
    public class SoftDeleteAndActivateProductCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
