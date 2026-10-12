namespace DGE.Core.Features.ProductMinerals.Dto
{
    public class ProductMineralFullDataDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int MineralId { get; set; }
        public string? MineralName { get; set; }
    }
}
