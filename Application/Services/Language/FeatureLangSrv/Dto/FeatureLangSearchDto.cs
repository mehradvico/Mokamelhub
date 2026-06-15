using Application.Common.Dto.Result;
using Application.Services.Language.FeatureLangSrv.Iface;
using Application.Services.Language.NameFieldLangSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Language.FeatureLangSrv.Dto
{
    public class FeatureLangSearchDto : BaseSearchDto<NameFieldLang, NameFieldLangDto>, IFeatureLangSearchFields
    {
        public FeatureLangSearchDto(FeatureLangInputDto dto, IQueryable<NameFieldLang> list, IMapper mapper) : base(dto, list, mapper)
        {
            FeatureId = dto.FeatureId;
        }

        public long FeatureId { get; set; }
    }
}
