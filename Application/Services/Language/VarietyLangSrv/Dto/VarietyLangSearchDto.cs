using Application.Common.Dto.Result;
using Application.Services.Language.NameFieldLangSrv.Dto;
using Application.Services.Language.VarietyLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Language.VarietyLangSrv.Dto
{
    public class VarietyLangSearchDto : BaseSearchDto<NameFieldLang, NameFieldLangDto>, IVarietyLangSearchFields
    {
        public VarietyLangSearchDto(VarietyLangInputDto dto, IQueryable<NameFieldLang> list, IMapper mapper) : base(dto, list, mapper)
        {
            VarietyId = dto.VarietyId;
        }

        public long VarietyId { get; set; }
    }
}
