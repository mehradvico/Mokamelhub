using Application.Common.Dto.Result;
using Application.Services.Content.ContactUsSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Content.ContactUsSrv.Iface
{
    public interface IContactUsService
    {
        Task<BaseResultDto<ContactUsDto>> FindAsyncDto(long id);
        Task<BaseResultDto<ContactUsVDto>> FindAsyncVDto(long id);
        Task<BaseResultDto> InsertAsyncDto(ContactUsDto dto);
        BaseResultDto Update(ContactUsDto dto);
        ContactUsSearchDto Search(ContactUsInputDto baseSearchDto);
    }
}
