using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Order.ProductOrderStoreOrderSrv.Dto;
using Application.Services.Order.ProductOrderStoreSrv.Dto;
using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Order.ProductOrderStoreSrv.Iface
{
    public interface IProductOrderStoreService : ICommonSrv<ProductOrderStore, ProductOrderStoreDto>
    {
        ProductOrderStoreSearchDto Search(ProductOrderStoreInputDto baseSearchDto);
        Task<BaseResultDto> InsertRangeAsyncDto(List<ProductOrderStoreDto> dtos);
    }
}

