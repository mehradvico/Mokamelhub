using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.BrandSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;


namespace Application.Services.ProductSrvs.BrandSrv.Dto
{
    public class BrandSearchDto : BaseSearchDto<Brand, BrandVDto>, IBrandSearchFields
    {
        public BrandSearchDto(BrandInputDto dto, IQueryable<Brand> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.StoreId = dto.StoreId;
            this.CategoryId = dto.CategoryId;

        }

        public long? StoreId { get; set; }
        public long? CategoryId { get; set; }
    }
}
