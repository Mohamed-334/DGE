using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductHarvestMonths.Commands.RequestModels
{
    public class AddProductHarvestMonthCommandRequestModel : IRequest<Response<string>>
    {
        public int? MonthStatusCode { get; set; }
        public int ProductId { get; set; }
    }
}
