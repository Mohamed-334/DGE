namespace DGE.Core.Features.Exhibitions.Dto
{
    public class ExhibitionFullDataDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
        public string? ExhibitionUrl { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public string? Location { get; set; }
        public string? Year { get; set; }
    }
}
