using System.ComponentModel.DataAnnotations;

namespace Application.Services.Accounting.UserSrv.Dto
{
    public class ChangeMobileDto
    {

        public long UserId { get; set; }
        [Display(Name = nameof(Resource.Field.Mobile), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Mobile { get; set; }
        [Display(Name = nameof(Resource.Field.Code), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Code { get; set; }
    }
}
