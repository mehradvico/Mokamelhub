using Application.Common.Dto.Field;
using Application.Services.Filing.PictureSrv.Dto;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.ProductSrvs.ProductPictureSrv.Dto
{
    public class ProductPictureDto : Id_FieldDto
    {
        public long ProductId { get; set; }
        [Display(Name = nameof(Resource.Field.Media), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
ErrorMessageResourceName = nameof(Resource.Pattern.PleaseSelectT1))]
        public long PictureId { get; set; }
        [Display(Name = nameof(Resource.Field.Label), ResourceType = typeof(Resource.Field))]

        public string Label { get; set; }
        public PictureVDto Picture { get; set; }

    }
}
