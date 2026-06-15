using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Common.Interface;
using Application.Common.Service;
using Application.Services.Content.ContactUsGroupSrv.Dto;
using Application.Services.Content.ContactUsGroupSrv.Iface;
using Application.Services.Dto;
using AutoMapper;
using Entities.Entities;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Content.ContactUsGroupSrv
{
    public class ContactUsGroupService : CommonSrv<ContactUsGroup, ContactUsGroupDto>, IContactUsGroupService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly CurrentUserDto _currentUserDto;
        public ContactUsGroupService(IDataBaseContext _context, IMapper mapper, ICurrentUserHelper currentUserHelper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
            this._currentUserDto = currentUserHelper.CurrentUser;
        }

        public BaseSearchDto<ContactUsGroupDto> Search(BaseInputDto searchDto)
        {
            var query = _context.ContactUsGroups.AsQueryable();

            if (searchDto.Available == true)
            {
                query = query.Where(s => s.Active);
            }

            if (!string.IsNullOrEmpty(searchDto.Q))
            {
                query = query.Where(s => s.Name.Contains(searchDto.Q));
            }
            if (searchDto.SortBy != Common.Enumerable.SortEnum.Default)
            {
                switch (searchDto.SortBy)
                {
                    case Common.Enumerable.SortEnum.Default:
                        {
                            query = query.OrderBy(s => s.Priority);
                            break;
                        }
                    case Common.Enumerable.SortEnum.New:
                        {
                            query = query.OrderByDescending(s => s.Id);
                            break;
                        }
                    case Common.Enumerable.SortEnum.Old:
                        {
                            query = query.OrderBy(s => s.Id);
                            break;
                        }
                    case Common.Enumerable.SortEnum.Name:
                        {
                            query = query.OrderByDescending(s => s.Name);
                            break;
                        }

                    case Common.Enumerable.SortEnum.MorePriority:
                        {
                            query = query.OrderByDescending(s => s.Priority);
                            break;
                        }
                    case Common.Enumerable.SortEnum.LessPriority:
                        {
                            query = query.OrderBy(s => s.Priority);
                            break;
                        }
                    default:
                        break;
                }
            }
            return new BaseSearchDto<ContactUsGroup, ContactUsGroupDto>(searchDto, query, mapper);
        }
        public BaseResultDto GetForRole()
        {
            var model = _context.ContactUsGroups.Where(s => s.Active).AsQueryable();

            if (_currentUserDto.RoleEnum != RoleEnum.Admin.ToString())
            {
                model = model.Where(s => s.Roles.Contains(_currentUserDto.RoleEnum));
            }
            return new BaseResultDto<List<ContactUsGroupDto>>(true, mapper.Map<List<ContactUsGroupDto>>(model));
        }

    }
}
