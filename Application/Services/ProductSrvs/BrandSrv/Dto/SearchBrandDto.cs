using Application.Common.Dto.Field;
using Application.Services.Filing.PictureSrv.Dto;

namespace Application.Services.ProductSrvs.BrandSrv.Dto
{
    public class SearchBrandDto : Name_FieldDto
    {
        public string SecondName { get; set; }
        public PictureVDto Icon { get; set; }

    }
}
