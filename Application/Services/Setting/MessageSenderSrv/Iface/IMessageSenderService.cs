using Application.Common.Enumerable.Message;
using System;
using System.Threading.Tasks;

namespace Application.Services.Setting.MessageSenderSrv.Iface
{
    public interface IMessageSenderService
    {
        Task SendMessageAsync(MessageTypeEnum messageType, string mobileReceptor, string emailReceptor, string body = null, string token1 = null, string token2 = null, string token3 = null, string token4 = null, string token5 = null, DateTime? sendDate = null);
    }
}
