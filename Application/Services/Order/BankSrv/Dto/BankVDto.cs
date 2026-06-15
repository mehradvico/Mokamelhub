using Application.Common.Dto.Field;
using Application.Services.Filing.PictureSrv.Dto;

namespace Application.Services.Order.BankSrv.Dto
{
    public class BankVDto : Name_FieldDto
    {
        public long? PictureId { get; set; }
        public string Label { get; set; }
        public PictureVDto Picture { get; set; }
    }

}
