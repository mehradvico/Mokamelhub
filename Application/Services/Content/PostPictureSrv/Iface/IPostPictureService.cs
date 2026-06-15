using Application.Common.Interface;
using Application.Services.Content.PostPictureSrv.Dto;
using Entities.Entities;
using System.Collections.Generic;

namespace Application.Services.Content.PostPictureSrv.Iface
{
    public interface IPostPictureService : ICommonSrv<PostPicture, PostPictureDto>
    {
        PostPictureSearchDto Search(PostPictureInputDto searchDto);
        void InsertOrUpdate(PostPictureDto PostPicture);
        void InsertOrUpdate(Post post, List<PostPictureDto> PostPicturesDto);
    }
}
