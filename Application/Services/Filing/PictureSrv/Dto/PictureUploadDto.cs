using Microsoft.AspNetCore.Http;

namespace Application.Services.Filing.PictureSrv.Dto
{
    public class PictureUploadDto
    {
        public IFormFile PictureFile { get; set; }
    }
}
