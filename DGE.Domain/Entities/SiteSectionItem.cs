using DGE.Domain.Shared.BaseEntity.Implementations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DGE.Domain.Entities
{
    public class SiteSectionItem : BaseEntity
    {
        [ForeignKey("SiteSection")]
        public int SiteSectionId { get; set; }
        public SiteSection? SiteSection { get; set; }
        public string? Title { get; set; }
        public string? TitleLocalization { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public string? Icon { get; set; }
        public int DisplayOrder { get; set; }
    }
}
