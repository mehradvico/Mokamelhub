using Application.Common.Dto.Result;
using Application.Services.Order.CartSrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Order.CartSrv.Iface
{
    public interface ICartService
    {
        Task<BaseResultDto> CartUpdateAsync(CartUpdateDto cartUpdate);

    }
}
