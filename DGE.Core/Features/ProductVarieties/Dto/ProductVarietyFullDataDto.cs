namespace DGE.Core.Features.ProductVarieties.Dto
{
    public class ProductVarietyFullDataDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
    }
}
