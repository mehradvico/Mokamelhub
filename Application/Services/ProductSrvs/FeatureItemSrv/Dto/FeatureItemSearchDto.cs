using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.FeatureItemSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.ProductSrvs.FeatureItemSrv.Dto
{
    public class FeatureItemSearchDto : BaseSearchDto<FeatureItem, FeatureItemDto>, IFeatureItemSearchFields
    {
        public FeatureItemSearchDto(FeatureItemInputDto dto, IQueryable<FeatureItem> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.FeatureId = dto.FeatureId;
        }


        public long FeatureId { get; set; }
    }
}
