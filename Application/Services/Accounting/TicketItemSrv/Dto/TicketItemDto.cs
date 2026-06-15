using Application.Common.Dto.Field;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Accounting.TicketItemSrv.Dto
{
    public class TicketItemDto : Id_FieldDto
    {

        [Display(Name = nameof(Resource.Field.Text), ResourceType = typeof(Resource.Field))]
        [Required(ErrorMessageResourceType = typeof(Resource.Pattern),
ErrorMessageResourceName = nameof(Resource.Pattern.PleaseInsertT1))]
        public string Body { get; set; }
        public DateTime CreateDate { get; set; }
        public long? FileId { get; set; }
        public long TicketId { get; set; }
        public long UserId { get; set; }


    }
}
