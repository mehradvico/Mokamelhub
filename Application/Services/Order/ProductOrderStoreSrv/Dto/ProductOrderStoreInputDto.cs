

using Application.Common.Dto.Input;
using Application.Services.Order.ProductOrderStoreSrv.Iface;

namespace Application.Services.Order.ProductOrderStoreSrv.Dto
{
    public class ProductOrderStoreInputDto : BaseInputDto, IProductOrderStoreSearchFields
    {
        public string ProductOrderId { get; set; }
        public long? StoreId { get; set; }
        public long? UserId { get; set; }

    }
}
