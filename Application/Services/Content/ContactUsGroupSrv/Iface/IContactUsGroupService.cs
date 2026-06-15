using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Content.ContactUsGroupSrv.Dto;
using Entities.Entities;

namespace Application.Services.Content.ContactUsGroupSrv.Iface
{
    public interface IContactUsGroupService : ICommonSrv<ContactUsGroup, ContactUsGroupDto>
    {
        BaseSearchDto<ContactUsGroupDto> Search(BaseInputDto baseSearchDto);
        BaseResultDto GetForRole();
    }
}
