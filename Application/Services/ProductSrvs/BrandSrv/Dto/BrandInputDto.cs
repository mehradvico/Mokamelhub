using Application.Common.Dto.Input;
using Application.Services.ProductSrvs.BrandSrv.Iface;

namespace Application.Services.ProductSrvs.BrandSrv.Dto
{
    public class BrandInputDto : BaseInputDto, IBrandSearchFields
    {
        public long? StoreId { get; set; }
        public long? CategoryId { get; set; }


    }
}
