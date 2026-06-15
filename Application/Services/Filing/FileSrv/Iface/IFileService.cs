using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Services.Filing.FileSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Filing.FileSrv.Iface
{
    public interface IFileService
    {
        Task<BaseResultDto> InsertAsyncDto(FileDto dto);
        Task<BaseResultDto> FindVDtoAsync(long id);

        BaseSearchDto<FileVDto> Search(BaseInputDto baseInputDto);
    }
}
