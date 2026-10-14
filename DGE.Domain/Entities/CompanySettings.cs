using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Domain.Entities
{
    // A single row holding the company details shown in the footer and the contact page
    public class CompanySettings : BaseEntity
    {
        public List<string> Phones { get; set; } = [];
        public List<string> Emails { get; set; } = [];
        public string? Whatsapp { get; set; }
        public string? Address { get; set; }
        public string? AddressLocalization { get; set; }
        public string? MapUrl { get; set; }
        public string? WorkingHours { get; set; }
        public string? WorkingHoursLocalization { get; set; }
        // Inbox for new contact-us messages; not returned to anonymous callers
        public string? NotificationEmail { get; set; }
    }
}
