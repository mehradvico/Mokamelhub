using Application.Common.Dto.Input;
using Application.Services.Language.PostLangSrv.Iface;

namespace Application.Services.Language.PostLangSrv.Dto
{
    public class PostLangInputDto : BaseInputDto, IPostLangSearchFields
    {
        public long PostId { get; set; }
    }
}
