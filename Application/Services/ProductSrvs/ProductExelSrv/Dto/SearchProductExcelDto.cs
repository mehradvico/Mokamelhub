namespace Application.Services.ProductSrvs.ProductExelSrv.Dto
{
    public class SearchProductExcelDto
    {
        public long? CategoryId { get; set; }
        public long? StoreId { get; set; }
        public int? ExpireDays { get; set; }
    }
}
