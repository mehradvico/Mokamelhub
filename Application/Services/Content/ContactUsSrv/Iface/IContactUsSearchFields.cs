namespace Application.Services.Content.ContactUsSrv.Iface
{
    public interface IContactUsSearchFields
    {
        public long? ContactUsGroupId { get; set; }
        public string ContactUsGroupLabel { get; set; }
        public bool? Status { get; set; }

    }
}
