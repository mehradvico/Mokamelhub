using Application.Common.Dto.OtherLanguage;
using Application.Common.Dto.Result;
using Application.Services.Language.SeoFieldLangSrv.Dto;
using Application.Services.Language.StoreLangSrv.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Language.StoreLangSrv.Iface
{
    public interface IStoreLangService
    {
        Task<BaseResultDto> FindAsyncDto(long id);
        Task<BaseResultDto> InsertAndUpdateAsyncDto(StoreLangDto dto);
        BaseResultDto DeleteDto(OtherLangDeleteDto dto);
        StoreLangSearchDto SearchDto(StoreLangInputDto dto);
    }
}
