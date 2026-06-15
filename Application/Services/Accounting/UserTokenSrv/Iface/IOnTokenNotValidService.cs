using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace Application.Services.Accounting.UserTokenSrv.Iface
{
    public interface IOnTokenNotValidService
    {
        Task Execute(AuthenticationFailedContext context);
    }
}
