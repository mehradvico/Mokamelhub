using Application.Common.Dto.Field;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Setting.CodeSrv.Dto
{
    public class CodeDto : Name_FieldDto
    {
        [Display(Name = nameof(Resource.Field.Label), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
        ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Label { get; set; }
        [Display(Name = nameof(Resource.Field.Value), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
         ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Value { get; set; }
        public long CodeGroupId { get; set; }
        [Display(Name = nameof(Resource.Field.Priority), ResourceType = typeof(Resource.Field))]
        public int Priority { get; set; }
        [Display(Name = nameof(Resource.Field.Active), ResourceType = typeof(Resource.Field))]
        public bool Active { get; set; }

    }
}
