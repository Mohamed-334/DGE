namespace DGE.Core.Features.SiteSections.Dto
{
    public class SiteSectionFullDataDto
    {
        // 0 while the section has not been saved yet
        public int Id { get; set; }
        public string? Key { get; set; }
        public string? Title { get; set; }
        public string? TitleLocalization { get; set; }
        public string? Details { get; set; }
        public string? DetailsLocalization { get; set; }
        public string? Image { get; set; }
        public List<SiteSectionItemDto> Items { get; set; } = [];
    }
}
