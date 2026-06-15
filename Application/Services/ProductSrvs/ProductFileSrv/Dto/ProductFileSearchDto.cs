using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.ProductFileSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.ProductSrvs.ProductFileSrv.Dto
{
    public class ProductFileSearchDto : BaseSearchDto<ProductFile, ProductFileVDto>, IProductFileSearchFields
    {
        public ProductFileSearchDto(ProductFileInputDto dto, IQueryable<ProductFile> list, IMapper mapper) : base(dto, list, mapper)
        {
            ProductId = dto.ProductId;
            ParentId = dto.ParentId;
            UserId = dto.UserId;
        }

        public long? ProductId { get; set; }
        public long? ParentId { get; set; }
        public long? UserId { get; set; }

    }
}
