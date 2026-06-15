using Application.Common.Enumerable.Message;
using System;
using System.Threading.Tasks;

namespace Application.Services.Setting.SmsSrv.Iface
{
    public interface ISmsService
    {
        Task SendSmsAsync(MessageTypeEnum smsType, string receptor, string body = null, string token1 = null, string token2 = null, string token3 = null, string token4 = null, string token5 = null, DateTime? sendDate = null);
        Task SendSmsGroupAsync(int pageSize = 100);
    }
}
