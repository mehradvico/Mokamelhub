using Application.Common.Dto.Field;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.ProductSrvs.ProductFileSrv.Dto
{
    public class ProductFileDto : Name_FieldDto
    {
        public long ProductId { get; set; }
        public long? FileId { get; set; }
        [Display(Name = nameof(Resource.Field.Label), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
        ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Label { get; set; }
        [Display(Name = nameof(Resource.Field.Protected), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
        ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public bool Protected { get; set; }
        public long? ParentId { get; set; }
        public int Priority { get; set; }
    }
}
