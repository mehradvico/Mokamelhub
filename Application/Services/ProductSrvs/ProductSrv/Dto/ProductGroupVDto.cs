using Application.Services.Filing.PictureSrv.Dto;
using Application.Services.Setting.CodeSrv.Dto;

namespace Application.Services.ProductSrvs.ProductSrv.Dto
{
    public class ProductGroupVDto
    {
        public string GroupName { get; set; }
        public string GroupTitle { get; set; }
        public int GroupPriority { get; set; }
        public long? PictureId { get; set; }
        public long StatusId { get; set; }
        public PictureVDto Picture { get; set; }
        public CodeDto Status { get; set; }
    }
}
