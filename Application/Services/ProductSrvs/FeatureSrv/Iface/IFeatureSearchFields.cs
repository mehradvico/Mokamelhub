namespace Application.Services.ProductSrvs.FeatureSrv.Iface
{
    public interface IFeatureSearchFields
    {
        public long? CategoryId { get; set; }
        public string CategoryLabel { get; set; }
        public bool? IsGroup { get; set; }
        public bool? IsHide { get; set; }
        public bool GetChildren { get; set; }

    }
}
