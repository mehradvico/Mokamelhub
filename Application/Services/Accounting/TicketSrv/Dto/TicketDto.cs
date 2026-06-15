using Application.Common.Dto.Field;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Application.Services.Accounting.TicketSrv.Dto
{
    public class TicketDto : Id_FieldDto
    {

        [Display(Name = nameof(Resource.Field.Title), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Name { get; set; }
        [Display(Name = nameof(Resource.Field.Text), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Body { get; set; }
        [Display(Name = nameof(Resource.Field.User), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
ErrorMessageResourceName = nameof(Resource.Pattern.PleaseSelectT1))]
        public long UserId { get; set; }
        public long? AdminId { get; set; }
        [IgnoreDataMember]
        public DateTime CreateDate { get; set; }
        [IgnoreDataMember]
        public DateTime UpdateDate { get; set; }
        public long? FileId { get; set; }
        public long StatusId { get; set; }
        public long ImportanceId { get; set; }
        public long? ProductId { get; set; }


    }
}
