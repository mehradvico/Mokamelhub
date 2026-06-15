using Application.Common.Dto.Result;
using Application.Services.CategorySrv.Dto;
using Application.Services.CategorySrv.Iface;
using Application.Services.Setting.CodeSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.CodeSrv.Dto
{
    public class CodeSearchDto : BaseSearchDto<Code, CodeVDto>, ICodeSearchFields
    {
        public CodeSearchDto(CodeInputDto dto, IQueryable<Code> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.CodeGroupLabel = dto.CodeGroupLabel;
        }

        public string CodeGroupLabel { get; set; }
    }
}
