using Application.Common.Dto.Field;
using Application.Services.Filing.PictureSrv.Dto;

namespace Application.Services.ProductSrvs.StoreSrv.Dto
{
    public class StoreMinVDto : Name_FieldDto
    {
        public long StoreId { get; set; }
        public int MaxDiscountPercent { get; set; }
        public double RateAvg { get; set; }
        public int RateCount { get; set; }
        public PictureVDto Picture { get; set; }
    }
}
