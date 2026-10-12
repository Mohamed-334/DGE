namespace DGE.Core.Features.ProductPackings.Dto
{
    public class ProductPackingFullDataDto
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? PackingTypeId { get; set; }
        public string? PackingTypeName { get; set; }
    }
}
