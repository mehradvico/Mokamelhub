using Application.Common.Dto.Input;
using Application.Services.Language.CodeLangSrv.Iface;

namespace Application.Services.Language.CodeLangSrv.Dto
{
    public class CodeLangInputDto : BaseInputDto, ICodeLangSearchFields
    {
        public long CodeId { get; set; }
    }
}
