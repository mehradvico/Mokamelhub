using System.ComponentModel.DataAnnotations;

namespace Application.Common.Dto.Field
{
    public class Name_FieldDto : Id_FieldDto
    {
        [Display(Name = nameof(Resource.Field.Name), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
        ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Name { get; set; }

    }
}
