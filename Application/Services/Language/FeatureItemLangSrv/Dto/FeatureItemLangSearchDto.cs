using Application.Common.Dto.Result;
using Application.Services.Language.FeatureItemLangSrv.Iface;
using Application.Services.Language.NameFieldLangSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Language.FeatureItemLangSrv.Dto
{
    public class FeatureItemLangSearchDto : BaseSearchDto<NameFieldLang, NameFieldLangDto>, IFeatureItemLangSearchFields
    {
        public FeatureItemLangSearchDto(FeatureItemLangInputDto dto, IQueryable<NameFieldLang> list, IMapper mapper) : base(dto, list, mapper)
        {
            FeatureItemId = dto.FeatureItemId;
        }
        public long FeatureItemId { get; set; }
    }
}
