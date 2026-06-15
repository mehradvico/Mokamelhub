using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.StoreCommentSrv.Iface;
using AutoMapper;
using System.Linq;

namespace Application.Services.ProductSrvs.StoreCommentSrv.Dto
{
    public class StoreCommentSearchDto : BaseSearchDto<Entities.Entities.StoreComment, StoreCommentVDto>, IStoreCommentSearchFields
    {
        public StoreCommentSearchDto(StoreCommentInputDto dto, IQueryable<Entities.Entities.StoreComment> list, IMapper mapper) : base(dto, list, mapper)
        {
            StoreId = dto.StoreId;
            AllStatus = dto.AllStatus;
        }
        public long? StoreId { get; set; }
        public bool? AllStatus { get; set; }

    }
}
