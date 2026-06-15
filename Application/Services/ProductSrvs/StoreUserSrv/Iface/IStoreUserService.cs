using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.StoreUserSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.StoreSrvs.StoreUserSrv.Iface
{
    public interface IStoreUserService
    {
        Task<BaseResultDto> GetAllAsync(StoreUserDto storeUser);
        Task<BaseResultDto> InsertAsync(StoreUserDto storeUser);
        Task<BaseResultDto> RemoveAsync(StoreUserDto storeUser);
    }
}
