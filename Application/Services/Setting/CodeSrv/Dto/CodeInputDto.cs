using Application.Common.Dto.Input;
using Application.Services.CategorySrv.Iface;

namespace Application.Services.CategorySrv.Dto
{
    public class CodeInputDto : BaseInputDto, ICodeSearchFields
    {
        public string CodeGroupLabel { get; set; }
    }
}
