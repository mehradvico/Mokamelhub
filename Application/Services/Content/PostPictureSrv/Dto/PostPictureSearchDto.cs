using Application.Common.Dto.Result;
using Application.Services.Content.PostPictureSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Content.PostPictureSrv.Dto
{
    public class PostPictureSearchDto : BaseSearchDto<PostPicture, PostPictureDto>, IPostPictureSearchFields
    {
        public PostPictureSearchDto(PostPictureInputDto dto, IQueryable<PostPicture> list, IMapper mapper) : base(dto, list, mapper)
        {
            PostId = dto.PostId;
        }
        public long? PostId { get; set; }
    }
}
