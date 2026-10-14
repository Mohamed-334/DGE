namespace DGE.Core.Features.SiteSections.Dto
{
    public class SiteSectionItemDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? TitleLocalization { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public string? Icon { get; set; }
        public int DisplayOrder { get; set; }
    }
}
