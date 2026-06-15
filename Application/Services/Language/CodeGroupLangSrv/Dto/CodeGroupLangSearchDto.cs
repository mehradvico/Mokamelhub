using Application.Common.Dto.Result;
using Application.Services.Language.CodeGroupLangSrv.Iface;
using Application.Services.Language.NameFieldLangSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Language.CodeGroupLangSrv.Dto
{
    public class CodeGroupLangSearchDto : BaseSearchDto<NameFieldLang, NameFieldLangDto>, ICodeGroupLangSearchFields
    {
        public CodeGroupLangSearchDto(CodeGroupLangInputDto dto, IQueryable<NameFieldLang> list, IMapper mapper) : base(dto, list, mapper)
        {
            CodeGroupId = dto.CodeGroupId;
        }

        public long CodeGroupId { get; set; }
    }
}
