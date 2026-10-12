using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductCategories.Commands.RequestModels
{
    public class UpdateProductCategoryCommandRequestModel : BaseModelWithName, IRequest<Response<string>>
    {
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
    }
}
