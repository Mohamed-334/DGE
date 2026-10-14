using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Domain.Entities
{
    public class SocialMedia : BaseEntity
    {
        public static readonly string[] Platforms = ["facebook", "instagram", "linkedin", "x", "youtube", "tiktok", "whatsapp"];

        // One of Platforms, stored lowercase
        public string? Platform { get; set; }
        public string? Url { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
