using Microsoft.AspNetCore.Http;

namespace Application.Services.Filing.FileSrv.Dto
{
    internal class FileUploadDto
    {
        public IFormFile File { get; set; }
    }
}
