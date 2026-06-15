using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.ProductReportSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.ProductSrvs.ProductReportSrv.Dto
{
    public class ProductReportSearchDto : BaseSearchDto<ProductReport, ProductReportVDto>, IProductReportSearchFields
    {
        public ProductReportSearchDto(ProductReportInputDto dto, IQueryable<ProductReport> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.ProductId = dto.ProductId;
            this.UserId = dto.UserId;
        }
        public long? ProductId { get; set; }
        public long? UserId { get; set; }
    }
}
