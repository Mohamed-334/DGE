namespace DGE.Core.Features.ProductVitamins.Dto
{
    public class ProductVitaminFullDataDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int VitaminId { get; set; }
        public string? VitaminName { get; set; }
    }
}
