using Application.Common.Dto.Result;
using Application.Services.CommonSrv.SearchSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.CommonSrv.SearchSrv.Iface
{
    public interface ISearchService
    {
        public Task<BaseResultDto<SearchDto>> SearchAsync(SearchRequestDto request);
    }
}
