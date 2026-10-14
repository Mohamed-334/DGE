using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Domain.Entities
{
    public class SiteSection : BaseEntity
    {
        public static readonly string[] Keys = ["welcome", "about", "quality", "export"];

        // One of Keys, stored lowercase
        public string? Key { get; set; }
        public string? Title { get; set; }
        public string? TitleLocalization { get; set; }
        // HTML rich text
        public string? Details { get; set; }
        public string? DetailsLocalization { get; set; }
        public string? Image { get; set; }
        public ICollection<SiteSectionItem>? Items { get; set; }
    }
}
