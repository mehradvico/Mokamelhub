using Application.Common.Interface;
using Application.Services.ProductSrvs.VarietyItemSrv.Dto;
using Entities.Entities;

namespace Application.Services.ProductSrvs.VarietyItemSrv.Iface
{
    public interface IVarietyItemService : ICommonSrv<VarietyItem, VarietyItemDto>
    {

        VarietyItemSearchDto SearchDto(VarietyItemInputDto searchDto);
    }
}
