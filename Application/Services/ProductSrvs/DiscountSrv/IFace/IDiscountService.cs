using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.DiscountSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.DiscountSrv.IFace
{
    public interface IDiscountService
    {
        Task<BaseResultDto> InsertAsync(DiscountDto discount);
        Task<BaseResultDto> ActiveAsync(DiscountDto discount);
        Task<BaseResultDto> DeleteAsync(DiscountDto discount);
        Task<BaseResultDto> FindAsyncDto(long id);
        DiscountSearchDto Search(DiscountInputDto searchDto);
        Task SyncExpiredAsync();

    }
}
