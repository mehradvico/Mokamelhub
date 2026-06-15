using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Common.Enumerable.Message;
using Application.Common.Helpers;
using Application.Common.Interface;
using Application.Common.Service;
using Application.Services.Accounting.TicketItemSrv.Dto;
using Application.Services.Accounting.TicketItemSrv.Iface;
using Application.Services.Dto;
using Application.Services.Setting.CodeSrv.Iface;
using Application.Services.Setting.MessageSenderSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.TicketItemSrv
{
    public class TicketItemService : CommonSrv<TicketItem, TicketItemDto>, ITicketItemService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly CurrentUserDto _currentUser;
        private readonly ICodeService _codeService;
        private readonly IMessageSenderService _messageSenderService;

        public TicketItemService(IDataBaseContext _context, IMapper mapper, ICurrentUserHelper currentUserHelper, ICodeService codeService, IMessageSenderService messageSenderService) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
            this._currentUser = currentUserHelper.CurrentUser;
            this._codeService = codeService;
            this._messageSenderService = messageSenderService;
        }
        public TicketItemSearchDto Search(TicketItemInputDto baseSearchDto)
        {
            var query = _context.TicketItems.Include(s => s.User).ThenInclude(s => s.Role).Include(s => s.File).AsQueryable();
            query = query.Where(s => s.TicketId == baseSearchDto.TicketId).OrderBy(s => s.Id).AsQueryable();
            return new TicketItemSearchDto(baseSearchDto, query, mapper);
        }
        public async Task<BaseResultDto> InsertAdminAsyncDto(TicketItemDto dto)
        {

            dto.UserId = _currentUser.UserId;
            dto.CreateDate = DateTime.Now;
            var insert = await base.InsertAsyncDto(dto);
            if (insert.IsSuccess)
            {
                var answeredStatus = await _codeService.GetByLabelAsync(TicketStatusEnum.TicketStatus_Answered.ToString());

                var ticket = _context.Tickets.Include(s => s.User).AsTracking().FirstOrDefault(s => s.Id == dto.TicketId);
                ticket.StatusId = answeredStatus.Id;
                ticket.UpdateDate = DateTime.Now;
                _context.Tickets.Update(ticket);
                _context.SaveChanges();
                var userFullName = string.Format("{0} {1}", ticket.User.FirstName, ticket.User.LastName);
                await _messageSenderService.SendMessageAsync(messageType: MessageTypeEnum.TicketAnswerd, mobileReceptor: ticket.User.Mobile, emailReceptor: ticket.User.Email, token1: userFullName, token2: ticket.Id.ToString(), sendDate: DateTime.Now);
            }
            return insert;
        }
        public async Task<BaseResultDto> InsertUserAsyncDto(TicketItemDto dto)
        {
            var closeStatus = await _codeService.GetByLabelAsync(TicketStatusEnum.TicketStatus_Close.ToString());

            var ticket = await _context.Tickets.AsTracking().FirstOrDefaultAsync(s => s.Id == dto.TicketId && s.Deleted == false && s.StatusId != closeStatus.Id);
            if (ticket == null)
            {
                return new BaseResultDto(false);
            }

            if (ticket.UserId != _currentUser.UserId || ticket.StatusId == closeStatus.Id)
            {
                return new BaseResultDto(false);

            }
            dto.UserId = _currentUser.UserId;
            dto.CreateDate = DateTime.Now;
            var insert = await base.InsertAsyncDto(dto);
            if (insert.IsSuccess)
            {
                var waitStatus = await _codeService.GetByLabelAsync(TicketStatusEnum.TicketStatus_Waiting.ToString());

                ticket.StatusId = waitStatus.Id;
                ticket.UpdateDate = DateTime.Now;
                _context.Tickets.Update(ticket);
                _context.SaveChanges();
                await _messageSenderService.SendMessageAsync(messageType: MessageTypeEnum.UserRegisterTicket, mobileReceptor: _currentUser.Mobile, emailReceptor: _currentUser.Email, token1: _currentUser.FullName, token2: ticket.Id.ToString(), sendDate: DateTime.Now);

            }
            return insert;
        }
        public override async Task<BaseResultDto<TicketItemDto>> InsertAsyncDto(TicketItemDto dto)
        {
            dto.Body = await SanitizeTextHelper.ToSanitizeAsync(dto.Body);
            return await base.InsertAsyncDto(dto);
        }

    }
}
