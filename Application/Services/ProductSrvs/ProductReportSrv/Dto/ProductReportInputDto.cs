using Application.Common.Dto.Input;
using Application.Services.ProductSrvs.ProductReportSrv.Iface;

namespace Application.Services.ProductSrvs.ProductReportSrv.Dto
{
    public class ProductReportInputDto : BaseInputDto, IProductReportSearchFields
    {
        public long? ProductId { get; set; }
        public long? UserId { get; set; }
    }
}
