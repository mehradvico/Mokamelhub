using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Accounting.UserProductSrv.Dto;
using Entities.Entities;
using System.Threading.Tasks;

namespace Application.Services.Accounting.UserProductSrv.Iface
{
    public interface IUserProductService : ICommonSrv<UserProduct, UserProductDto>
    {
        UserProductSearchDto SearchDto(UserProductInputDto dto);
        Task<bool> UserHasProductAsync(long productId, long userId);
        Task<BaseResultDto> InsertOrderItemAsyncDto(ProductOrder productOrder);

        Task<BaseResultDto<long>> UserProductCountAsync();

    }
}
