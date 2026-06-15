using Application.Common.Dto.Field;
using System;

namespace Application.Services.Content.NewsletterSrv.Dto
{
    public class NewsletterDto : Id_FieldDto
    {
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
