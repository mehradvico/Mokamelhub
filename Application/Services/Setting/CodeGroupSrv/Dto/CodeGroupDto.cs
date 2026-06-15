using Application.Common.Dto.Field;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Setting.CodeGroupSrv.Dto
{
    public class CodeGroupDto : Name_FieldDto
    {
        [Display(Name = nameof(Resource.Field.Label), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
  ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Label { get; set; }
    }
}
