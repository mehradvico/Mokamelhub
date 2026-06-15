using Application.Common.Enumerable.Message;
using System;
using System.Threading.Tasks;

namespace Application.Services.Setting.EmailSrv.Iface
{
    public interface IEmailService
    {
        Task SendEmailAsync(MessageTypeEnum emailType, string receptor, string body = null, string token1 = null, string token2 = null, string token3 = null, string token4 = null, string token5 = null, DateTime? sendDate = null);
        Task SendEmailGroupAsync(int pageSize = 100);
    }
}
