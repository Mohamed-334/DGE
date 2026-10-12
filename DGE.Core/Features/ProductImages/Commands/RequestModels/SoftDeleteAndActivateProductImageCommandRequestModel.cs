using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductImages.Commands.RequestModels
{
    public class SoftDeleteAndActivateProductImageCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
