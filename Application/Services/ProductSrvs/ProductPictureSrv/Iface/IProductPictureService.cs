using Application.Common.Interface;
using Application.Services.ProductSrvs.ProductPictureSrv.Dto;
using Entities.Entities;
using System.Collections.Generic;

namespace Application.Services.ProductSrvs.ProductPictureSrv.Iface
{
    public interface IProductPictureService : ICommonSrv<ProductPicture, ProductPictureDto>
    {
        ProductPictureSearchDto SearchDto(ProductPictureInputDto dto);
        void InsertOrUpdate(ProductPictureDto productPicture);
        void InsertOrUpdate(Product product, List<ProductPictureDto> productPicturesDto);
    }
}
