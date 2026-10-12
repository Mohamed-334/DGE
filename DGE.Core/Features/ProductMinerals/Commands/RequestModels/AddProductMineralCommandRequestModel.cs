using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductMinerals.Commands.RequestModels
{
    public class AddProductMineralCommandRequestModel : IRequest<Response<string>>
    {
        public int ProductId { get; set; }
        public int MineralId { get; set; }
    }
}
