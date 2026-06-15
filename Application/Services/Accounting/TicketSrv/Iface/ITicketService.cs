using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Accounting.TicketSrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.Accounting.TicketSrv.Iface
{
    public interface ITicketService : ICommonSrv<Ticket, TicketDto>
    {
        TicketSearchDto Search(TicketInputDto baseSearchDto);
        Task<BaseResultDto<TicketDto>> InsertAdminAsyncDto(TicketDto dto);
        Task<BaseResultDto<TicketDto>> InsertUserAsyncDto(TicketDto dto);
        BaseResultDto ChangeStatus(TicketDto dto);
        BaseResultDto ChangeAdmin(TicketDto dto);
        BaseResultDto ChangeImportance(TicketDto dto);
        Task<BaseResultDto<TicketVDto>> FindAsyncVDto(long id, long? adminId = null);
        Task<BaseResultDto<TicketVDto>> UserFindAsyncVDto(long id);
        Task CloseTicketAsync(int hours = 24);
    }
}
