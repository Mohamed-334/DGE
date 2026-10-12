namespace DGE.Core.Features.ProductImages.Dto
{
    public class ProductImageFullDataDto
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ImagePath { get; set; }
    }
}
