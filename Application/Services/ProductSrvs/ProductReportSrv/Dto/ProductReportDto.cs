using Application.Common.Dto.Field;

namespace Application.Services.ProductSrvs.ProductReportSrv.Dto
{
    public class ProductReportDto : Id_FieldDto
    {
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public string ReportDetail { get; set; }

    }
}
