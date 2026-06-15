using Application.Common.Dto.Field;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Accounting.PermissionSrv.Dto
{
    public class PermissionDto : Id_FieldDto
    {
        [Display(Name = nameof(Resource.Field.Name), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Name { get; set; }
        [Display(Name = nameof(Resource.Field.Label), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
      ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Label { get; set; }
        [Display(Name = nameof(Resource.Field.Area), ResourceType = typeof(Resource.Field))]
        public string Area { get; set; }
        [Display(Name = nameof(Resource.Field.Controller), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
      ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Controller { get; set; }
        [Display(Name = nameof(Resource.Field.Action), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
      ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Action { get; set; }
        [Display(Name = nameof(Resource.Field.IsMenu), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
      ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public bool IsMenu { get; set; }
        [Display(Name = nameof(Resource.Field.Priority), ResourceType = typeof(Resource.Field))]

        public int Priority { get; set; }
        public long? ParentId { get; set; }

    }
}
