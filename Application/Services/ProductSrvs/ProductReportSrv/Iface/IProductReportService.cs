using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.ProductSrvs.ProductReportSrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.ProductReportSrv.Iface
{
    public interface IProductReportService : ICommonSrv<ProductReport, ProductReportDto>
    {
        Task<BaseResultDto<ProductReportVDto>> FindAsyncVDto(long id);
        ProductReportSearchDto Search(ProductReportInputDto baseSearchDto);
    }
}
