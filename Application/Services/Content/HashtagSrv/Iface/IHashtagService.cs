using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Content.HashtagSrv.Dto;
using Entities.Entities;

namespace Application.Services.Content.HashtagSrv.Iface
{
    public interface IHashtagService : ICommonSrv<Hashtag, HashtagDto>
    {
        BaseSearchDto<HashtagDto> Search(BaseInputDto baseSearchDto);
        Hashtag GetOrAddByName(string name);
    }
}
