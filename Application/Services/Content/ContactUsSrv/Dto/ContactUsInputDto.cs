using Application.Common.Dto.Input;
using Application.Services.Content.ContactUsSrv.Iface;

namespace Application.Services.Content.ContactUsSrv.Dto
{
    public class ContactUsInputDto : BaseInputDto, IContactUsSearchFields
    {
        public long? ContactUsGroupId { get; set; }
        public string ContactUsGroupLabel { get; set; }
        public bool? Status { get; set; }
    }
}
