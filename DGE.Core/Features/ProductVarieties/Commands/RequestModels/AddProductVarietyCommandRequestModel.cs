using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductVarieties.Commands.RequestModels
{
    public class AddProductVarietyCommandRequestModel : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
        public int ProductId { get; set; }
    }
}
