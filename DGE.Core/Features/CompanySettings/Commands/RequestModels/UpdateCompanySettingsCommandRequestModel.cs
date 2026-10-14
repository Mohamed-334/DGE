using DGE.Core.Shared.Models;
using MediatR;

namespace DGE.Core.Features.CompanySettings.Commands.RequestModels
{
    public class UpdateCompanySettingsCommandRequestModel : IRequest<Response<string>>
    {
        public List<string>? Phones { get; set; }
        public List<string>? Emails { get; set; }
        public string? Whatsapp { get; set; }
        public string? Address { get; set; }
        public string? AddressLocalization { get; set; }
        public string? MapUrl { get; set; }
        public string? WorkingHours { get; set; }
        public string? WorkingHoursLocalization { get; set; }
        public string? NotificationEmail { get; set; }
    }
}
