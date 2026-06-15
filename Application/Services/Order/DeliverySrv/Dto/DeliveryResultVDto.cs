using Application.Common.Dto.Field;
using Application.Services.Setting.CodeSrv.Dto;

namespace Application.Services.Order.DeliverySrv.Dto
{
    public class DeliveryResultVDto : Id_FieldDto
    {
        public long DeliveryTypeId { get; set; }
        public double DeliveryPrice { get; set; }
        public string DeliveryPriceString { get; set; }
        public double? MinPriceForFree { get; set; }
        public string MinPriceForFreeString { get; set; }
        public string Description { get; set; }
        public int MaxDays { get; set; }
        public string MaxDaysString { get; set; }
        public int MinCountForFree { get; set; }
        public bool AfterRent { get; set; }
        public long? StoreId { get; set; }

        public CodeVDto DeliveryType { get; set; }

    }
}
