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
        public List<ProductImageItemDto>? Images { get; set; }
        public List<ProductLookupItemDto>? Varieties { get; set; }
        public List<ProductLookupItemDto>? Types { get; set; }
        public List<ProductLookupItemDto>? PackingTypes { get; set; }
        public List<ProductLookupItemDto>? Vitamins { get; set; }
        public List<ProductLookupItemDto>? Minerals { get; set; }
        public List<ProductHarvestMonthItemDto>? HarvestMonths { get; set; }
    }
}
