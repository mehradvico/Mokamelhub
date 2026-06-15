using Application.Common.Dto.Result;
using Application.Services.Content.PostFileSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Content.PostFileSrv.Dto
{
    public class PostFileSearchDto : BaseSearchDto<PostFile, PostFileDto>, IPostFileSearchFields
    {
        public PostFileSearchDto(PostFileInputDto dto, IQueryable<PostFile> list, IMapper mapper) : base(dto, list, mapper)
        {
            PostId = dto.PostId;
        }
        public long? PostId { get; set; }
    }
}
