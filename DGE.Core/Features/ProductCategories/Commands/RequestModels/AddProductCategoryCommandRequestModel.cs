using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductCategories.Commands.RequestModels
{
    public class AddProductCategoryCommandRequestModel : IRequest<Response<string>>
    {
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
    }
}
