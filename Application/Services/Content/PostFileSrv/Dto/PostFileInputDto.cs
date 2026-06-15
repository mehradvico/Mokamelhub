using Application.Common.Dto.Input;
using Application.Services.Content.PostFileSrv.Iface;

namespace Application.Services.Content.PostFileSrv.Dto
{
    public class PostFileInputDto : BaseInputDto, IPostFileSearchFields
    {
        public long? PostId { get; set; }
    }
}
