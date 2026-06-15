using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.FeatureSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.ProductSrvs.VarietyItemSrv.Dto
{
    public class VarietyItemSearchDto : BaseSearchDto<VarietyItem, VarietyItemVDto>, IVarietyItemSearchFields
    {
        public VarietyItemSearchDto(VarietyItemInputDto dto, IQueryable<VarietyItem> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.VarietyId = dto.VarietyId;

        }

        public long VarietyId { get; set; }

    }
}
