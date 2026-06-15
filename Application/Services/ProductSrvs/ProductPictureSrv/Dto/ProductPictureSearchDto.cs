using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.ProductPictureSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.ProductSrvs.ProductPictureSrv.Dto
{
    public class ProductPictureSearchDto : BaseSearchDto<ProductPicture, ProductPictureVDto>, IProductPictureSearchFields
    {
        public ProductPictureSearchDto(ProductPictureInputDto dto, IQueryable<ProductPicture> list, IMapper mapper) : base(dto, list, mapper)

        {
            ProductId = dto.ProductId;
        }
        public long? ProductId { get; set; }
    }
}
