namespace DGE.Core.Features.ProductTypes.Dto
{
    public class ProductTypeFullDataDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int TypeId { get; set; }
        public string? TypeName { get; set; }
    }
}
