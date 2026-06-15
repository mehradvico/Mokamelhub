using Application.Common.Dto.Field;
using Application.Services.ProductSrvs.StoreSrv.Dto;
using Application.Services.ProductSrvs.VarietyItemSrv.Dto;

namespace Application.Services.ProductSrvs.ProductItemSrv.Dto
{
    public class ProductItemDto : Id_FieldDto
    {
        public long BasePrice { get; set; }
        public long WholeSalePrice { get; set; }
        public long? VarietyItemId { get; set; }
        public long? VarietyItem2Id { get; set; }
        public long ProductId { get; set; }
        public long StoreId { get; set; }
        public int Quantity { get; set; }
        public bool Active { get; set; }
        public string Warranty { get; set; }
        public bool SystemActive { get; set; }

        public VarietyItemVDto VarietyItem { get; set; }
        public VarietyItemVDto VarietyItem2 { get; set; }
        public StoreMinVDto Store { get; set; }
    }
}
