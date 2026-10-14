using DGE.Core.Features.Dashboard.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.Dashboard.Queries.RequestModels
{
    public class GetDashboardStatsQueryRequestModel : IRequest<Response<DashboardStatsDto>>
    {
    }
}
