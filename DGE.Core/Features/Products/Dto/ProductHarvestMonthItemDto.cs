namespace DGE.Core.Features.Products.Dto
{
    public class ProductHarvestMonthItemDto
    {
        // Id of the product harvest month row, used to delete it
        public int Id { get; set; }
        public int? Code { get; set; }
        public string? Month { get; set; }
        public string? DisplayName { get; set; }
    }
}
