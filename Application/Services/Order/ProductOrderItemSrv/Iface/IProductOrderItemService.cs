using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Order.ProductOrderItemSrv.Dto;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Order.ProductOrderItemSrv.Iface
{
    public interface IProductOrderItemService : ICommonSrv<ProductOrderItem, ProductOrderItemDto>
    {
        BaseSearchDto<ProductOrderItemVDto> Search(ProductOrderItemInputDto baseSearchDto);
        Task<BaseResultDto> InsertRangeAsyncDto(List<ProductOrderItemDto> dtos);
    }
}

