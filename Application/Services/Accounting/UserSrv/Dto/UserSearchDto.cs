using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Services.Accounting.UserSrv.Iface;
using AutoMapper;
using Entities.Entities.Security;
using System.Linq;

namespace Application.Services.Dto
{
    public class UserSearchDto : BaseSearchDto<User, UserVDto>, IUserSearchFields
    {
        public UserSearchDto(UserInputDto dto, IQueryable<User> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.RoleEnum = dto.RoleEnum;
            this.RoleId = dto.RoleId;
        }

        public long? RoleId { get; set; }
        public RoleEnum? RoleEnum { get; set; }
    }
}
