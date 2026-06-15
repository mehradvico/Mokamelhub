using Application.Common.Dto.Result;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Common.Interface
{
    public interface ICommonSrv<TEntity, TDto>
    {
        Task<BaseResultDto<TDto>> FindAsyncDto(long id);
        Task<BaseResultDto<TDto>> FirstOrDefaultAsyncDto();
        Task<BaseResultDto<TDto>> FirstOrDefaultAsyncDto(Expression<Func<TEntity, bool>> predicate);
        Task<BaseResultDto<TDto>> InsertAsyncDto(TDto dto);
        BaseResultDto UpdateDto(TDto dto);
        BaseResultDto UpdateRangeDto(List<TDto> dtoList);
        BaseResultDto DeleteDto(long id);
        BaseResultDto DeleteDto(TDto dto);
    }
}
