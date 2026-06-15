using Application.Common.Dto.Result;
using Application.Services.Content.ContactUsSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System.Linq;

namespace Application.Services.Content.ContactUsSrv.Dto
{
    public class ContactUsSearchDto : BaseSearchDto<ContactUs, ContactUsVDto>, IContactUsSearchFields
    {
        public ContactUsSearchDto(ContactUsInputDto dto, IQueryable<ContactUs> list, IMapper mapper) : base(dto, list, mapper)
        {
            this.ContactUsGroupId = dto.ContactUsGroupId;
            this.ContactUsGroupLabel = dto.ContactUsGroupLabel;
            this.Status = dto.Status;
        }

        public long? ContactUsGroupId { get; set; }
        public string ContactUsGroupLabel { get; set; }
        public bool? Status { get; set; }

    }
}
