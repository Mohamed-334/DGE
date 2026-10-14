namespace DGE.Core.Features.SocialMedias.Dto
{
    public class SocialMediaFullDataDto
    {
        public int Id { get; set; }
        public string? Platform { get; set; }
        public string? Url { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
    }
}
