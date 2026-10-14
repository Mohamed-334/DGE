using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Products.Commands.RequestModels
{
    public class ToggleShowOnHomeProductCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
