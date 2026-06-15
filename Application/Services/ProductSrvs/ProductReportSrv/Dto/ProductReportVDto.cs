using Application.Common.Dto.Field;
using Application.Services.Dto;
using Application.Services.ProductSrvs.ProductSrv.Dto;

namespace Application.Services.ProductSrvs.ProductReportSrv.Dto
{
    public class ProductReportVDto : Id_FieldDto
    {
        public long ProductId { get; set; }
        public long UserId { get; set; }
        public string ReportDetail { get; set; }

        public ProductMinVDto Product { get; set; }
        public UserMinVDto User { get; set; }
    }
}
