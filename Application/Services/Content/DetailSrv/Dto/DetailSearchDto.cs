using Application.Common.Dto.Result;
using Application.Services.Content.DetailSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Content.DetailSrv.Dto
{
    public class DetailSearchDto : BaseSearchDto<Detail, DetailVDto>, IDetailSearchFields
    {
        public DetailSearchDto(DetailInputDto dto, IQueryable<Detail> list, IMapper mapper) : base(dto, list, mapper)
        {

            this.CategoryId = dto.CategoryId;
            this.CategoryLabel = dto.CategoryLabel;
            this.Label = dto.Label;
        }

        public long? CategoryId { get; set; }
        public string CategoryLabel { get; set; }

        public string Label { get; set; }

    }
}
