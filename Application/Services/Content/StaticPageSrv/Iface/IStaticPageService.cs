using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Content.StaticPageSrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.Content.StaticPageSrv.Iface
{
    public interface IStaticPageService : ICommonSrv<StaticPage, StaticPageDto>
    {
        BaseSearchDto<StaticPageVDto> Search(StaticPageInputDto searchDto);
        Task<BaseResultDto> GetByLabelAsync(string label);

    }
}
