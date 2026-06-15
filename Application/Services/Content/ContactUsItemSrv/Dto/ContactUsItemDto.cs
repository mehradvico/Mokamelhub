using Entities.Entities.CommonField;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Dto.Field;

namespace Application.Services.Content.ContactUsItemSrv.Dto
{
    public class ContactUsItemDto : Id_FieldDto
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public long ContactUsId { get; set; }
    }
}
