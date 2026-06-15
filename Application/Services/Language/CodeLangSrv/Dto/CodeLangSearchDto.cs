using Application.Common.Dto.Result;
using Application.Services.Language.CodeLangSrv.Iface;
using Application.Services.Language.NameFieldLangSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Language.CodeLangSrv.Dto
{
    public class CodeLangSearchDto : BaseSearchDto<NameFieldLang, NameFieldLangDto>, ICodeLangSearchFields
    {
        public CodeLangSearchDto(CodeLangInputDto dto, IQueryable<NameFieldLang> list, IMapper mapper) : base(dto, list, mapper)
        {
            CodeId = dto.CodeId;
        }

        public long CodeId { get; set; }
    }
}
