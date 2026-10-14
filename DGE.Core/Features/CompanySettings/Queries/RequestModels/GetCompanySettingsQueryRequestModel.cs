using DGE.Core.Features.CompanySettings.Dto;
using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.CompanySettings.Queries.RequestModels
{
    public class GetCompanySettingsQueryRequestModel : IRequest<Response<CompanySettingsFullDataDto>>
    {
    }
}
