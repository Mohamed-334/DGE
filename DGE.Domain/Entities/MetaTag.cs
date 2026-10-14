using DGE.Domain.Shared.BaseEntity.Implementations;

namespace DGE.Domain.Entities
{
    public class MetaTag : BaseEntity
    {
        public static readonly string[] Pages = ["home", "about", "products", "certifications", "exhibitions", "export", "quality", "contact"];

        // One of Pages, stored lowercase
        public string? Page { get; set; }
        public string? Title { get; set; }
        public string? TitleLocalization { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public string? Keywords { get; set; }
        public string? OgImage { get; set; }
    }
}
