using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductTypes.Commands.RequestModels
{
    public class SoftDeleteAndActivateProductTypeCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
