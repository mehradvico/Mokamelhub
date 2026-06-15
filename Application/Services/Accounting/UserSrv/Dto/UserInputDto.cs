using Application.Common.Dto.Input;
using Application.Common.Enumerable;
using Application.Services.Accounting.UserSrv.Iface;

namespace Application.Services.Dto
{
    public class UserInputDto : BaseInputDto, IUserSearchFields
    {
        public long? RoleId { get; set; }
        public RoleEnum? RoleEnum { get; set; }
    }
}
