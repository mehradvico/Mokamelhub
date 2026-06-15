using Application.Common.Dto.Result;
using Application.Services.Content.PostCommentSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Content.PostCommentSrv.Dto
{
    public class PostCommentSearchDto : BaseSearchDto<PostComment, PostCommentVDto>, IPostCommentSearchFields
    {
        public PostCommentSearchDto(PostCommentInputDto dto, IQueryable<PostComment> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.PostId = dto.PostId;
            this.AllStatus = dto.AllStatus;
        }
        public long? PostId { get; set; }
        public bool? AllStatus { get; set; }

    }
}
