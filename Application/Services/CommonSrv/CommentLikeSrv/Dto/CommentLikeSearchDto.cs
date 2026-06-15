using Application.Common.Dto.Result;
using Application.Services.CommonSrv.CommentLikeSrv.Iface;
using Application.Services.CommonSrv.CommentSrv.Dto;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.CommonSrv.CommentLikeSrv.Dto
{
    public class CommentLikeSearchDto : BaseSearchDto<Comment, CommentVDto>, ICommentLikeSearchFields
    {
        public CommentLikeSearchDto(CommentLikeInputDto dto, IQueryable<Comment> list, IMapper mapper) : base(dto, list, mapper)
        {
            CommentId = dto.CommentId;
        }

        public long CommentId { get; set; }
    }
}
