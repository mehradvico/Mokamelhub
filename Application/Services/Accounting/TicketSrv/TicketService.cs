using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Common.Helpers;
using Application.Common.Interface;
using Application.Common.Service;
using Application.Services.Accounting.TicketSrv.Dto;
using Application.Services.Accounting.TicketSrv.Iface;
using Application.Services.Accounting.UserSrv.Iface;
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

namespace Application.Services.TicketSrv
{
    public class TicketService : CommonSrv<Ticket, TicketDto>, ITicketService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly CurrentUserDto _currentUser;
        private readonly ICodeService _codeService;
        private readonly IMessageSenderService _messageSenderService;
        private readonly IUserService _userService;

        public TicketService(IDataBaseContext _context, IMapper mapper, ICurrentUserHelper currentUserHelper, ICodeService codeService, IMessageSenderService messageSenderService, IUserService userService) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
            this._currentUser = currentUserHelper.CurrentUser;
            this._codeService = codeService;
            this._messageSenderService = messageSenderService;
            this._userService = userService;
        }
        public async Task<BaseResultDto<TicketVDto>> FindAsyncVDto(long id, long? adminId = null)
        {

            var item = await _context.Tickets.Include(s => s.User).Include(s => s.Admin).Include(s => s.File).FirstOrDefaultAsync(s => s.Id == id);
            if (item != null)
            {
                if (adminId.HasValue && item.AdminId != adminId)
                {
                    return new BaseResultDto<TicketVDto>(false, null);
                }
                return new BaseResultDto<TicketVDto>(true, mapper.Map<TicketVDto>(item));
            }
            return new BaseResultDto<TicketVDto>(false, null);

        }
        public async Task<BaseResultDto<TicketVDto>> UserFindAsyncVDto(long id)
        {

            var item = await _context.Tickets.Include(s => s.User).Include(s => s.Admin).Include(s => s.File).FirstOrDefaultAsync(s => s.Id == id);
            if (item != null && item.UserId == _currentUser.UserId)
            {
                return new BaseResultDto<TicketVDto>(true, mapper.Map<TicketVDto>(item));
            }
            return new BaseResultDto<TicketVDto>(false, null);

        }
        public TicketSearchDto Search(TicketInputDto baseSearchDto)
        {
            var query = _context.Tickets.Include(s => s.Product).Include(s => s.Status).Include(s => s.User).Include(s => s.Admin).ThenInclude(s => s.Role).Include(s => s.File).AsQueryable();

            if (baseSearchDto.UserId.HasValue)
            {
                query = query.Where(s => s.UserId == baseSearchDto.UserId);
            }
            if (baseSearchDto.AllAdminId == false && baseSearchDto.AdminId.HasValue)
            {
                query = query.Where(s => s.AdminId == baseSearchDto.AdminId);
            }
            if (baseSearchDto.Status.HasValue)
            {
                query = query.Where(s => s.Status.Label == baseSearchDto.Status.ToString());
            }
            if (baseSearchDto.Importance.HasValue)
            {
                query = query.Where(s => s.Importance.Label == baseSearchDto.Importance.ToString());
            }

            if (baseSearchDto.DateFrom.HasValue)
            {
                query = query.Where(s => s.CreateDate >= baseSearchDto.DateFrom);
            }
            if (baseSearchDto.DateTo.HasValue)
            {
                query = query.Where(s => s.CreateDate <= baseSearchDto.DateTo);
            }
            if (!string.IsNullOrEmpty(baseSearchDto.Q))
            {
                query = query.Where(s => s.Name.Contains(baseSearchDto.Q) || s.User.FirstName.Contains(baseSearchDto.Q) || s.User.LastName.Contains(baseSearchDto.Q) || s.Id.ToString() == baseSearchDto.Q);
            }
            switch (baseSearchDto.SortBy)
            {
                case Common.Enumerable.SortEnum.Default:
                    {
                        query = query.OrderByDescending(s => s.UpdateDate);
                        break;
                    }
                case Common.Enumerable.SortEnum.New:
                    {
                        query = query.OrderByDescending(s => s.UpdateDate);
                        break;
                    }
                case Common.Enumerable.SortEnum.Old:
                    {
                        query = query.OrderBy(s => s.UpdateDate);
                        break;
                    }

                default:
                    break;
            }
            return new TicketSearchDto(baseSearchDto, query, mapper);
        }

        public override async Task<BaseResultDto<TicketDto>> InsertAsyncDto(TicketDto dto)
        {
            dto.Body = await SanitizeTextHelper.ToSanitizeAsync(dto.Body);
            return await base.InsertAsyncDto(dto);
        }
        public async Task<BaseResultDto<TicketDto>> InsertAdminAsyncDto(TicketDto dto)
        {
            dto.AdminId = _currentUser.UserId;
            dto.CreateDate = dto.UpdateDate = DateTime.Now;
            dto.StatusId = dto.StatusId;
            dto.ImportanceId = dto.ImportanceId;
            var result = await base.InsertAsyncDto(dto);
            if (result.IsSuccess)
            {
                var user = _userService.GetVDto(dto.UserId);
                await _messageSenderService.SendMessageAsync(messageType: Common.Enumerable.Message.MessageTypeEnum.AdminRegisterTicket, mobileReceptor: user.Mobile, emailReceptor: user.Email, token1: user.FullName, token2: result.Data.Id.ToString(), sendDate: DateTime.Now);

            }
            return result;
        }
        public async Task<BaseResultDto<TicketDto>> InsertUserAsyncDto(TicketDto dto)
        {
            var waitStatus = await _codeService.GetByLabelAsync(TicketStatusEnum.TicketStatus_Waiting.ToString());
            var ticketImportanceNormal = await _codeService.GetByLabelAsync(TicketImportanceEnum.TicketImportance_Normal.ToString());

            dto.UserId = _currentUser.UserId;
            dto.AdminId = null;
            dto.CreateDate = dto.UpdateDate = DateTime.Now;
            dto.StatusId = waitStatus.Id;
            dto.ImportanceId = ticketImportanceNormal.Id;
            var result = await base.InsertAsyncDto(dto);
            if (result.IsSuccess)
            {
                await _messageSenderService.SendMessageAsync(messageType: Common.Enumerable.Message.MessageTypeEnum.UserRegisterTicket, mobileReceptor: _currentUser.Mobile, emailReceptor: _currentUser.Email, token1: _currentUser.FullName, token2: result.Data.Id.ToString(), sendDate: DateTime.Now);
            }
            return result;
        }
        public override BaseResultDto UpdateDto(TicketDto dto)
        {
            var item = FindAsyncDto(dto.Id).Result;
            item.Data.Name = dto.Name;
            item.Data.Body = dto.Body;
            item.Data.FileId = dto.FileId;
            return base.UpdateDto(item.Data);
        }
        public BaseResultDto ChangeStatus(TicketDto dto)
        {
            var item = FindAsyncDto(dto.Id).Result;
            item.Data.StatusId = dto.StatusId;
            return base.UpdateDto(item.Data);
        }
        public BaseResultDto ChangeImportance(TicketDto dto)
        {
            var item = FindAsyncDto(dto.Id).Result;
            item.Data.ImportanceId = dto.ImportanceId;
            return base.UpdateDto(item.Data);
        }
        public BaseResultDto ChangeAdmin(TicketDto dto)
        {
            var item = FindAsyncDto(dto.Id).Result;
            item.Data.AdminId = dto.AdminId;
            return base.UpdateDto(item.Data);
        }

        public async Task CloseTicketAsync(int hours = 24)
        {
            var closeCode = await _codeService.GetByLabelAsync(TicketStatusEnum.TicketStatus_Close.ToString());
            var AnsweredCode = await _codeService.GetByLabelAsync(TicketStatusEnum.TicketStatus_Answered.ToString());
            if (closeCode != null)
            {
                _context.Tickets.Where(s => s.StatusId == AnsweredCode.Id && s.UpdateDate.AddHours(hours) < DateTime.Now).ExecuteUpdate(s => s.SetProperty(p => p.StatusId, closeCode.Id));
                await _context.SaveChangesAsync();

            }
        }
    }
}
