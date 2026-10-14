namespace DGE.Core.Features.CompanySettings.Dto
{
    public class CompanySettingsFullDataDto
    {
        public List<string> Phones { get; set; } = [];
        public List<string> Emails { get; set; } = [];
        public string? Whatsapp { get; set; }
        public string? Address { get; set; }
        public string? AddressLocalization { get; set; }
        public string? MapUrl { get; set; }
        public string? WorkingHours { get; set; }
        public string? WorkingHoursLocalization { get; set; }
        // Only filled for signed-in users
        public string? NotificationEmail { get; set; }
    }
}
