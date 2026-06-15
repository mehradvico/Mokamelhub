using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.FeatureSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.ProductSrvs.FeatureSrv.Dto
{
    public class FeatureSearchDto : BaseSearchDto<Feature, FeatureVDto>, IFeatureSearchFields
    {
        public FeatureSearchDto(FeatureInputDto dto, IQueryable<Feature> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.CategoryId = dto.CategoryId;
            this.CategoryLabel = dto.CategoryLabel;
            this.IsGroup = dto.IsGroup;
            this.IsHide = dto.IsHide;
            this.GetChildren = dto.GetChildren;


        }

        public long? CategoryId { get; set; }
        public string CategoryLabel { get; set; }
        public bool? IsGroup { get; set; }
        public bool GetChildren { get; set; }
        public bool? IsHide { get; set; }


    }
}
