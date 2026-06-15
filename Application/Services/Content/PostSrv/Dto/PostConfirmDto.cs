using Application.Common.Dto.Field;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Content.PostSrv.Dto
{
    public class PostConfirmDto : Id_FieldDto
    {

        [Display(Name = nameof(Resource.Field.PublishDate), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public DateTime PublishDate { get; set; }
        public bool AdminConfirm { get; set; }

    }
}
