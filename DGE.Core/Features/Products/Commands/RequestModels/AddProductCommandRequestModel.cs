using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Products.Commands.RequestModels
{
    public class AddProductCommandRequestModel : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
