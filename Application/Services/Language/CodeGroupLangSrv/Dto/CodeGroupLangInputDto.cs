using Application.Common.Dto.Input;
using Application.Services.Language.CodeGroupLangSrv.Iface;

namespace Application.Services.Language.CodeGroupLangSrv.Dto
{
    public class CodeGroupLangInputDto : BaseInputDto, ICodeGroupLangSearchFields
    {
        public long CodeGroupId { get; set; }
    }
}
