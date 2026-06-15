using Application.Common.Dto.Result;
using Application.Services.Language.NameFieldLangSrv.Dto;
using Application.Services.Language.StateLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Language.StateLangSrv.Dto
{
    public class StateLangSearchDto : BaseSearchDto<NameFieldLang, NameFieldLangDto>, IStateLangSearchFields
    {
        public StateLangSearchDto(StateLangInputDto dto, IQueryable<NameFieldLang> list, IMapper mapper) : base(dto, list, mapper)
        {
            StateId = dto.StateId;
        }
        public long StateId { get; set; }
    }
}
