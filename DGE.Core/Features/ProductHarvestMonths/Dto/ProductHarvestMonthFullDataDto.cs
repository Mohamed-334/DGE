namespace DGE.Core.Features.ProductHarvestMonths.Dto
{
    public class ProductHarvestMonthFullDataDto
    {
        public int Id { get; set; }
        public int? MonthStatusCode { get; set; }
        public string? MonthStatus { get; set; }
        public string? MonthStatusDisplayName { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
    }
}
