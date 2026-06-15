using Application.Common.Dto.Input;
using Application.Services.ProductSrvs.ProductFileSrv.Iface;

namespace Application.Services.ProductSrvs.ProductFileSrv.Dto
{
    public class ProductFileInputDto : BaseInputDto, IProductFileSearchFields
    {
        public long? ProductId { get; set; }
        public long? UserId { get; set; }
        public long? ParentId { get; set; }

    }
}
