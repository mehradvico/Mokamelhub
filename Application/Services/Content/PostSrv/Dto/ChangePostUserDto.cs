using Application.Common.Dto.Field;

namespace Application.Services.Content.PostSrv.Dto
{
    public class ChangePostUserDto : Id_FieldDto
    {
        public long UserId { get; set; }

    }
}
