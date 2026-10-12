using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductMinerals.Commands.RequestModels
{
    public class DeleteProductMineralCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
