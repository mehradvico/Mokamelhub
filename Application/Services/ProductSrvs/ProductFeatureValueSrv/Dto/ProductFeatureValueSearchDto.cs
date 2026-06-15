using Application.Common.Dto.Input;
using Application.Common.Enumerable;
using Application.Services.ProductSrvs.ProductFeatureValueSrv.Dto;
using Application.Services.ProductSrvs.ProductFeatureValueSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services.ProductSrvs.FeatureItemSrv.Dto
{
    public class ProductFeatureValueSearchDto : BaseInputDto, IProductFeatureValueSearchFields
    {
        public ProductFeatureValueSearchDto(ProductFeatureValueInputDto dto, IQueryable<IGrouping<Feature, ProductFeatureValue>> list, IMapper mapper)
        {
            this.ProductId = dto.ProductId;
            this.FeatureTypeEnum = dto.FeatureTypeEnum;
            this.Hide = dto.Hide;
            base.PageIndex = dto.PageIndex;
            base.PageSize = dto.PageSize;
            base.Q = dto.Q;
            base.Available = dto.Available;
            base.SortBy = dto.SortBy;
            this.TotalCount = list.Count();
            var skip = (dto.PageIndex - 1) * dto.PageSize;
            foreach (var item in list)
            {
                var listItem = mapper.Map<ProductFeatureVDto>(item.Key);
                listItem.ProductFeatureValues = mapper.Map<List<ProductFeatureValueMinVDto>>(item.ToList());
                List.Add(listItem);
            }

        }


        public long ProductId { get; set; }
        public FeatureTypeEnum? FeatureTypeEnum { get; set; }
        public bool? Hide { get; set; }
        public int TotalCount { get; set; }

        public List<ProductFeatureVDto> List { get; set; } = new List<ProductFeatureVDto>() { };
    }
}
