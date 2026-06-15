using Application.Common.Dto.Result;
using Application.Services.Accounting.OtpVerifySrv.Dto;
using System.Threading.Tasks;

namespace Application.Services.Accounting.OtpVerifySrv.Iface
{
    public interface IOtpVerifyService
    {
        Task<BaseResultDto<OtpVerifyVDto>> InsertAsyncDto(OtpVerifyVDto dto);
        Task<BaseResultDto> CheckVerify(OtpVerifyVDto dto);
        Task<BaseResultDto> IsVerified(OtpVerifyVDto dto);
    }
}
