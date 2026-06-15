using Application.Common.Dto.Input;
using Application.Services.Content.PostPictureSrv.Iface;

namespace Application.Services.Content.PostPictureSrv.Dto
{
    public class PostPictureInputDto : BaseInputDto, IPostPictureSearchFields
    {
        public long? PostId { get; set; }
    }
}
