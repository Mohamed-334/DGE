namespace DGE.Core.Features.Products.Dto
{
    public class ProductLookupItemDto
    {
        // Id of the linked item (type, vitamin, ...); for varieties it equals LinkId
        public int Id { get; set; }
        // Id of the product link row, used to delete the link
        public int LinkId { get; set; }
        public string? Name { get; set; }
        public string? NameLocalization { get; set; }
    }
}
