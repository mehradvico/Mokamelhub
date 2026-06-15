namespace Application.Services.Content.PostSrv.Iface
{
    public interface IPostSearchFields
    {
        public bool? Publish { get; set; }
        public string Hashtags { get; set; }
        public long[] CategoryIds { get; set; }
        public string[] CategoryLabels { get; set; }
        public bool IsAndCategories { get; set; }
        public bool? Active { get; set; }
        public bool? AdminConfirm { get; set; }
        public bool? AllAdminConfirm { get; set; }
        public bool? Edited { get; set; }
        public long? NotId { get; set; }

    }
}
