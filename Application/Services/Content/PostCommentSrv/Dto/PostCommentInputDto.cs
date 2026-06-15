using Application.Common.Dto.Input;
using Application.Services.Content.PostCommentSrv.Iface;

namespace Application.Services.Content.PostCommentSrv.Dto
{
    public class PostCommentInputDto : BaseInputDto, IPostCommentSearchFields
    {

        public long? PostId { get; set; }
        public bool? AllStatus { get; set; }

    }
}
