using Application.Common.Enumerable.Message;
using Application.Services.Setting.EmailSrv.Iface;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Application.Services.Setting.EmailSrv
{
    public class EmailService : IEmailService
    {
        private readonly IDataBaseContext _context;
        public EmailService(IDataBaseContext _context)
        {
            this._context = _context;
        }

        public async Task SendEmailGroupAsync(int pageSize = 100)
        {
            var justNow = DateTime.Now;
            var items = _context.Emails.Include(s => s.EmailType).Where(s => s.IsSend == false && s.Status == null && (s.SendDate == null || (s.SendDate.HasValue && s.SendDate.Value < justNow))).Take(pageSize).AsTracking();
            if (items.Any())
            {
                var groupType = items.GroupBy(s => s.EmailType);
                foreach (var type in groupType)
                {
                    var emailSetting = await _context.EmailSettings.Include(s => s.EmailType).Include(s => s.EmailAddress).ThenInclude(s => s.EmailHost).FirstOrDefaultAsync(s => s.EmailType.Label == type.Key.Label.ToString());
                    if (emailSetting != null)
                    {
                        foreach (var email in type)
                        {
                            await SendEmailAsync(emailSetting, email);
                        }
                    }
                    else
                    {
                        foreach (var email in type)
                        {
                            email.Status = false;
                            email.StatusText = Resource.Notification.SettingsAreNotComplete;
                            _context.Emails.Update(email);
                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
        }
        public async Task SendEmailAsync(MessageTypeEnum emailType, string receptor, string body = null, string token1 = null, string token2 = null, string token3 = null, string token4 = null, string token5 = null, DateTime? sendDate = null)
        {
            try
            {
                var emailSetting = await _context.EmailSettings.Include(s => s.EmailType).Include(s => s.EmailAddress).ThenInclude(s => s.EmailHost).FirstOrDefaultAsync(s => s.EmailType.Label == emailType.ToString());
                if (emailSetting != null)
                {
                    var receptors = receptor.Split(',');
                    foreach (var resp in receptors)
                    {
                        body = emailSetting.EmailType.Pattern.Replace("{{token1}}", token1).Replace("{{token2}}", token2).Replace("{{token3}}", token3).Replace("{{token4}}", token4).Replace("{{token5}}", token5);
                        var email = new Email() { SendDate = sendDate, CreateDate = DateTime.Now, Receptor = resp, EmailTypeId = emailSetting.EmailTypeId, Body = body, Title = emailSetting.EmailType.Name };
                        await _context.Emails.AddAsync(email);
                        await _context.SaveChangesAsync();
                        if (sendDate == null)
                        {
                            await SendEmailAsync(emailSetting, email);
                        }
                    }

                }
            }
            catch
            {

            }

        }
        private async Task SendEmailAsync(EmailSetting emailSetting, Email email)
        {
            email.IsSend = true;
            _context.Emails.Update(email);
            await _context.SaveChangesAsync();
            await SendSingleAsync(emailSetting.EmailAddress, email);
        }

        private async Task SendSingleAsync(EmailAddress emailAddress, Email email)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                NetworkCredential basicCredential = new NetworkCredential(emailAddress.Email.Trim(), emailAddress.Password.Trim());
                MailMessage message = new MailMessage();
                MailAddress fromAddress = new MailAddress(emailAddress.Email, emailAddress.EmailTitle);
                smtpClient.Host = emailAddress.EmailHost.Smtp;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = basicCredential;
                message.From = fromAddress;
                message.Subject = email.Title;
                message.IsBodyHtml = true;
                message.Body = email.Body;
                message.To.Add(email.Receptor);
                smtpClient.Send(message);
                email.SendDate = DateTime.Now;
                email.Sender = emailAddress.Email;
                _context.Emails.Update(email);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                email.Status = false;
                email.StatusText = ex.Message;
                _context.Emails.Update(email);
                await _context.SaveChangesAsync();
            }
        }
    }

}
