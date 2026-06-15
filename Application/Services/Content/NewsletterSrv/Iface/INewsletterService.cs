using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Interface;
using Application.Services.Content.NewsletterSrv.Dto;
using Entities.Entities;

namespace Application.Services.Content.NewsletterSrv.Iface
{
    public interface INewsletterService : ICommonSrv<Newsletter, NewsletterDto>
    {
        BaseSearchDto<NewsletterDto> Search(BaseInputDto baseSearchDto);
    }
}
