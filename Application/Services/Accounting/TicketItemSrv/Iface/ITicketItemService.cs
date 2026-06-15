using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Accounting.TicketItemSrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.Accounting.TicketItemSrv.Iface
{
    public interface ITicketItemService : ICommonSrv<TicketItem, TicketItemDto>
    {
        TicketItemSearchDto Search(TicketItemInputDto baseSearchDto);
        Task<BaseResultDto> InsertAdminAsyncDto(TicketItemDto dto);
        Task<BaseResultDto> InsertUserAsyncDto(TicketItemDto dto);
    }
}
