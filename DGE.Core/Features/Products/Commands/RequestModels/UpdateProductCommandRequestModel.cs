using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Products.Commands.RequestModels
{
    public class UpdateProductCommandRequestModel : BaseModelWithName, IRequest<Response<string>>
    {
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public int ProductCategoryId { get; set; }
    }
}
