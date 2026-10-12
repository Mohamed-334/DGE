using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.ProductHarvestMonths.Commands.RequestModels
{
    public class SoftDeleteAndActivateProductHarvestMonthCommandRequestModel : IRequest<Response<string>>
    {
        public int Id { get; set; }
    }
}
