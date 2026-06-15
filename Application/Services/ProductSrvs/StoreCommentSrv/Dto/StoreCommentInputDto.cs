using Application.Common.Dto.Input;
using Application.Services.ProductSrvs.StoreCommentSrv.Iface;

namespace Application.Services.ProductSrvs.StoreCommentSrv.Dto
{
    public class StoreCommentInputDto : BaseInputDto, IStoreCommentSearchFields
    {

        public long? StoreId { get; set; }
        public bool? AllStatus { get; set; }

    }
}
