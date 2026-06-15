using Application.Common.Dto.Result;
using Application.Services.Language.CityLangSrv.Iface;
using Application.Services.Language.NameFieldLangSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Language.CityLangSrv.Dto
{
    public class CityLangSearchDto : BaseSearchDto<NameFieldLang, NameFieldLangDto>, ISatetLangSearchFields
    {
        public CityLangSearchDto(CityLangInputDto dto, IQueryable<NameFieldLang> list, IMapper mapper) : base(dto, list, mapper)
        {
            CityId = dto.CityId;
        }
        public long CityId { get; set; }
    }
}
