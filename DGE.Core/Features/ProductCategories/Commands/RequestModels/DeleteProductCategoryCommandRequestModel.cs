using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductCategories.Commands.RequestModels
{
    public class DeleteProductCategoryCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
