using Application.Common.Interface;
using Application.Services.ProductSrvs.StoreCommentSrv.Dto;
using Entities.Entities;

namespace Application.Services.ProductSrvs.StoreCommentSrv.Iface
{
    public interface IStoreCommentService : ICommonSrv<StoreComment, StoreCommentDto>
    {
        StoreCommentSearchDto Search(StoreCommentInputDto baseSearchDto);
    }
}
