using Application.Services.ProductSrvs.VarietyItemSrv.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.ProductItemSrv.Dto
{
    public class ProductItemMinDto
    {
        public long Id { get; set; }
        public long? VarietyItemId { get; set; }
        public long? VarietyItem2Id { get; set; }
        public long BasePrice { get; set; }
        public long Price { get; set; }
        public long WholeSalePrice { get; set; }
        public int DiscountPercent { get; set; }
        public int Quantity { get; set; }

        public VarietyItemVDto VarietyItem { get; set; }
        public VarietyItemVDto VarietyItem2 { get; set; }
    }
}
