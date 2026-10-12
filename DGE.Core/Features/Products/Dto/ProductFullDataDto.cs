namespace DGE.Core.Features.Products.Dto
{
    public class ProductFullDataDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
        public string? Description { get; set; }
        public string? DescriptionLocalization { get; set; }
        public int ProductCategoryId { get; set; }
        public string? ProductCategoryName { get; set; }
    }
}
