using Application.Common.Interface;
using Application.Services.Content.PostFileSrv.Dto;
using Entities.Entities;
using System.Collections.Generic;

namespace Application.Services.Content.PostFileSrv.Iface
{
    public interface IPostFileService : ICommonSrv<PostFile, PostFileDto>
    {
        PostFileSearchDto Search(PostFileInputDto searchDto);
        void InsertOrUpdate(PostFileDto postFile);
        void InsertOrUpdate(Post post, List<PostFileDto> postFilesDto);
    }
}
