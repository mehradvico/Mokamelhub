using Application.Common.Dto.Field;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.CommonSrv.CommentSrv.Dto
{
    public class CommentDto : Id_FieldDto
    {
        public string Name { get; set; }

        [Display(Name = nameof(Resource.Field.Text), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
       ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Text { get; set; }
        public int? Rate { get; set; }
        public long StatusId { get; set; }
        public string Answer { get; set; }
        public long? UserId { get; set; }
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }
    }
}
