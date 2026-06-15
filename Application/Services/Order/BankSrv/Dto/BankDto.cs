using Application.Common.Dto.Field;

namespace Application.Services.Order.BankSrv.Dto
{
    public class BankDto : Name_FieldDto
    {
        public long? PictureId { get; set; }
        public string Label { get; set; }
        public string PaymentUrl { get; set; }
        public string VerficationUrl { get; set; }
        public string Verfication2Url { get; set; }
        public bool Active { get; set; }
    }

}
