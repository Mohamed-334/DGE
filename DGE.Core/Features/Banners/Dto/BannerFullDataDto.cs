namespace DGE.Core.Features.Banners.Dto
{
    public class BannerFullDataDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? TitleLocalization { get; set; }
        public string? Subtitle { get; set; }
        public string? SubtitleLocalization { get; set; }
        public string? Image { get; set; }
        public string? ButtonText { get; set; }
        public string? ButtonTextLocalization { get; set; }
        public string? Link { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
