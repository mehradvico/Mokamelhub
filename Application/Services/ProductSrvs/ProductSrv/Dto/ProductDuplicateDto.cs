namespace Application.Services.ProductSrvs.ProductSrv.Dto
{
    public class ProductDuplicateDto
    {
        public long ProductId { get; set; }
        public string CodeValue { get; set; }
        public long? StoreId { get; set; }
        public bool DuplicatePicture { get; set; }
        public bool DuplicateProductFeatureValues { get; set; }
        public bool DuplicateProductPictures { get; set; }
        //public bool DuplicateProductFiles { get; set; }
        //public bool DuplicateProductItems { get; set; }
        //public bool DuplicateSeoFieldLangs { get; set; }
    }
}
