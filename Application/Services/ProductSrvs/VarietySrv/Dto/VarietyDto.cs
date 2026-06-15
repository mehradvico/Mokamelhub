using Application.Common.Dto.Field;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.ProductSrvs.VarietySrv.Dto
{
    public class VarietyDto : Name_FieldDto
    {
        [Display(Name = nameof(Resource.Field.Description), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
         ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Description { get; set; }
        [Display(Name = nameof(Resource.Field.Priority), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
         ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public int Priority { get; set; }
        public bool InSearch { get; set; }
        [Display(Name = nameof(Resource.Field.Label), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
         ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Label { get; set; }
    }
}
