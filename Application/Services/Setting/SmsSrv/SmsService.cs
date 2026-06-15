using Application.Common.Enumerable.Message;
using Application.Common.Enumerable.Sms;
using Application.Services.Setting.SmsSrv.Iface;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence.Interface;
using RestSharp;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Setting.SmsSrv
{
    public class SmsService : ISmsService
    {
        private readonly IDataBaseContext _context;
        public SmsService(IDataBaseContext _context)
        {
            this._context = _context;
        }

        public async Task SendSmsGroupAsync(int pageSize = 100)
        {
            var justNow = DateTime.Now;

            var items = _context.Smses.Include(s => s.SmsType).Where(s => s.IsSend == false && s.Status == null && (s.SendDate == null || (s.SendDate.HasValue && s.SendDate.Value < justNow))).Take(pageSize).AsTracking();
            if (items.Any())
            {
                var groupType = items.GroupBy(s => s.SmsType);
                foreach (var type in groupType)
                {
                    var smsSetting = await _context.SmsSettings.Include(s => s.SmsType).Include(s => s.SmsNumber).ThenInclude(s => s.SmsProvider).FirstOrDefaultAsync(s => s.SmsType.Label == type.Key.Label.ToString());
                    if (smsSetting != null)
                    {
                        foreach (var sms in type)
                        {
                            await SendSmsAsync(smsSetting, sms);
                        }
                    }
                    else
                    {
                        foreach (var sms in type)
                        {
                            sms.Status = false;
                            sms.StatusText = Resource.Notification.SettingsAreNotComplete;
                            _context.Smses.Update(sms);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
        }
        public async Task SendSmsAsync(MessageTypeEnum smsType, string receptor, string body = null, string token1 = null, string token2 = null, string token3 = null, string token4 = null, string token5 = null, DateTime? sendDate = null)
        {
            var smsSetting = await _context.SmsSettings.Include(s => s.SmsType).Include(s => s.SmsNumber).ThenInclude(s => s.SmsProvider).FirstOrDefaultAsync(s => s.SmsType.Label == smsType.ToString());
            if (smsSetting != null)
            {
                var receptors = receptor.Split(',');
                foreach (var resp in receptors)
                {
                    var sms = new Sms() { SendDate = sendDate, CreateDate = DateTime.Now, Receptor = resp, SmsTypeId = smsSetting.SmsTypeId, Body = body, Token1 = token1, Token2 = token2, Token3 = token3, Token4 = token4, Token5 = token5 };
                    await _context.Smses.AddAsync(sms);
                    await _context.SaveChangesAsync();
                    if (sendDate == null)
                    {
                        await SendSmsAsync(smsSetting, sms);
                    }
                }
            }
        }
        private async Task SendSmsAsync(SmsSetting smsSetting, Sms sms)
        {
            sms.IsSend = true;
            _context.Smses.Update(sms);
            await _context.SaveChangesAsync();
            if (smsSetting.SmsNumber.SmsProvider.Label == SmsProviderEnum.Kavenegar.ToString())
            {
                await SendSingleKavenegarAsync(smsSetting.SmsNumber, sms);
            }
            else if (smsSetting.SmsNumber.SmsProvider.Label == SmsProviderEnum.KavenegarLookup.ToString())
            {
                await SendSingleKavenegarLookupAsync(smsSetting.SmsNumber, smsSetting.SmsType, sms);
            }
        }

        private async Task SendSingleKavenegarAsync(SmsNumber smsNumber, Sms sms)
        {
            try
            {
                var client = new RestClient(smsNumber.SmsProvider.ServiceUrl + smsNumber.ApiKey + "/sms/send.json");
                var request = new RestRequest("", Method.Get);
                request.AddQueryParameter("receptor", sms.Receptor);
                request.AddQueryParameter("message", sms.Body);
                request.AddQueryParameter("sender", smsNumber.Number);
                request.AddQueryParameter("localid", sms.Id.ToString());
                var response = await client.ExecuteAsync(request);
                dynamic result = JsonConvert.DeserializeObject(response.Content);
                sms.StatusText = response.Content;
                int status = result.@return.status;
                if (status == 200)
                {
                    sms.Status = true;
                }
                else
                    sms.Status = false;

                sms.SentDate = DateTime.Now;
                sms.Sender = smsNumber.Number;
                _context.Smses.Update(sms);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                sms.Status = false;
                sms.StatusText = e.Message;
                _context.Smses.Update(sms);
                await _context.SaveChangesAsync();
            }
        }

        private async Task SendSingleKavenegarLookupAsync(SmsNumber smsNumber, MessageType messageType, Sms sms)
        {
            try
            {
                string restClient = smsNumber.SmsProvider.ServiceUrl + smsNumber.ApiKey + "/verify/lookup.json";
                var client = new RestClient(restClient);
                string template = messageType.Label.Replace("_", "");
                var request = new RestRequest("", Method.Get);
                request.AddQueryParameter("receptor", sms.Receptor);
                request.AddQueryParameter("template", template);
                if (!string.IsNullOrWhiteSpace(sms.Token1))
                    request.AddQueryParameter("token", sms.Token1.Replace(" ", "_"));
                if (!string.IsNullOrWhiteSpace(sms.Token2))
                    request.AddQueryParameter("token2", sms.Token2.Replace(" ", "_"));
                if (!string.IsNullOrWhiteSpace(sms.Token3))
                    request.AddQueryParameter("token3", sms.Token3.Replace(" ", "_"));
                if (!string.IsNullOrWhiteSpace(sms.Token4))
                    request.AddQueryParameter("token10", sms.Token4.Replace(" ", "_"));
                if (!string.IsNullOrWhiteSpace(sms.Token5))
                    request.AddQueryParameter("token20", sms.Token5.Replace(" ", "_"));
                var response = await client.ExecuteAsync(request);
                dynamic result = JsonConvert.DeserializeObject(response.Content);
                sms.StatusText = response.Content;
                int status = result.@return.status;
                if (status == 200)
                {
                    sms.Status = true;
                }
                else
                    sms.Status = false;

                sms.SentDate = DateTime.Now;
                _context.Smses.Update(sms);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                sms.Status = false;
                sms.StatusText = e.Message;
                _context.Smses.Update(sms);
                await _context.SaveChangesAsync();
            }
        }
    }
}
