namespace DGE.Core.Features.MetaTags.Dto
{
    public class MetaTagFullDataDto
    {
        // 0 while the page has not been saved yet
        public int Id { get; set; }
        public string? Page { get; set; }
        public string? Title { get; set; }
        public string? TitleLocalization { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public string? Keywords { get; set; }
        public string? OgImage { get; set; }
    }
}
