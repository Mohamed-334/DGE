using BaseArchitecture.Domain.Shared.BaseEntity.Implementations;

namespace BaseArchitecture.Domain.Entities
{
    public class Otp : BaseEntity
    {
        public string? Email { get; set; }
        public string? OtpCode { get; set; }
        public DateTime? ExpiryTime { get; set; }
        public bool? IsUsed { get; set; } = false;
    }
}
