using Application.Common.Dto.Input;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Dto.Result
{
    public class BaseSearchDto<Tdto> : BaseInputDto
    {
        public BaseSearchDto()
        {
            List<Tdto> List = new List<Tdto>();
        }
        public BaseSearchDto(BaseInputDto baseSearchDto)
        {
            base.PageIndex = baseSearchDto.PageIndex;
            base.PageSize = baseSearchDto.PageSize;
            base.Q = baseSearchDto.Q;
            base.Available = baseSearchDto.Available;
            base.SortBy = baseSearchDto.SortBy;
            List<Tdto> List = new List<Tdto>();
        }

        public int TotalCount { get; set; }

        public List<Tdto> List { get; set; }
    }

    public class BaseSearchDto<TEntity, TDto> : BaseSearchDto<TDto>
    {
        public BaseSearchDto(BaseInputDto dto, IQueryable<TEntity> list, IMapper mapper) : base(dto)
        {
            base.TotalCount = list.Count();

            var skip = (dto.PageIndex - 1) * dto.PageSize;
            list = list.Skip(skip).Take(dto.PageSize);
            base.List = mapper.Map<List<TDto>>(list);
        }
    }
}
