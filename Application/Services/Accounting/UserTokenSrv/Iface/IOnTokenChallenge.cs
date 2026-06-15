using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;

namespace Application.Services.Accounting.UserTokenSrv.Iface
{
    public interface IOnTokenChallenge
    {
        Task Execute(JwtBearerChallengeContext context);
    }
}
