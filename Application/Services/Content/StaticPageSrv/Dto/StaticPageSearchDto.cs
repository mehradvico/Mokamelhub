using Application.Common.Dto.Result;
using Application.Services.Content.StaticPageSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Content.StaticPageSrv.Dto
{
    public class StaticPageSearchDto : BaseSearchDto<StaticPage, StaticPageVDto>, IStaticPageSearchFields
    {
        public StaticPageSearchDto(StaticPageInputDto dto, IQueryable<StaticPage> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.Label = dto.Label;
        }

        public string Label { get; set; }

    }
}
