using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Content.DetailSrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.Content.DetailSrv.Iface
{
    public interface IDetailService : ICommonSrv<Detail, DetailDto>
    {
        BaseSearchDto<DetailVDto> Search(DetailInputDto searchDto);
        Task<BaseResultDto<DetailVDto>> GetByLabelAsync(string label);

    }
}
