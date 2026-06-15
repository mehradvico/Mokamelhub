namespace Application.Services.Content.DetailSrv.Iface
{
    public interface IDetailSearchFields
    {
        public string Label { get; set; }
        public long? CategoryId { get; set; }
        public string CategoryLabel { get; set; }

    }
}
