using Application.Common.Dto.Result;
using Application.Services.Accounting.UserTokenSrv.Iface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Services.Accounting.UserTokenSrv.Srv
{
    public class OnTokenChallenge : IOnTokenChallenge
    {
        public async Task Execute(JwtBearerChallengeContext context)
        {
            if (context.Response.StatusCode == 200)
            {
                context.Response.StatusCode = 401;
                context.Response.ContentType = "text/plain";
                context.Response.WriteAsync(JsonSerializer.Serialize(new BaseResultDto(isSuccess: false, val: Resource.Notification.InvalidToken))).Wait();
            }
            context.HandleResponse();
            await Task.CompletedTask;
        }
    }
}
