using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.ProductSrvs.ProductFileSrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.ProductFileSrv.Iface
{
    public interface IProductFileService : ICommonSrv<ProductFile, ProductFileDto>
    {
        ProductFileSearchDto SearchDto(ProductFileInputDto dto);
        Task<BaseResultDto> GetForUserAsync(long productId, long? userId);
    }
}
