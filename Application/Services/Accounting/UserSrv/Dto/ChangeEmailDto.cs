using System.ComponentModel.DataAnnotations;

namespace Application.Services.Accounting.UserSrv.Dto
{
    public class ChangeEmailDto
    {

        public long UserId { get; set; }
        [Display(Name = nameof(Resource.Field.Email), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Email { get; set; }
        [Display(Name = nameof(Resource.Field.Code), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Code { get; set; }
    }
}
