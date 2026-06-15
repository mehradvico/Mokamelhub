using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.CommonSrv.SearchSrv.Dto;
using Application.Services.ProductSrvs.BrandSrv.Dto;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.BrandSrv.Iface
{
    public interface IBrandService : ICommonSrv<Brand, BrandDto>
    {
        public BrandSearchDto Search(BrandInputDto searchDto);
        Task<BaseResultDto<BrandVDto>> FindAsyncVDto(long id);
        BaseResultDto GetForCategory(string categorylabel);
        Task<List<SearchBrandDto>> SearchMinAsync(SearchRequestDto request);
    }
}
