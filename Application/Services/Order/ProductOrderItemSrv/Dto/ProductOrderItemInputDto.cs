

using Application.Common.Dto.Input;
using Application.Services.Order.ProductOrderItemSrv.Iface;

namespace Application.Services.Order.ProductOrderItemSrv.Dto
{
    public class ProductOrderItemInputDto : BaseInputDto, IProductOrderItemSearchFields
    {
        public long ProductOrderStoreId { get; set; }

    }
}
