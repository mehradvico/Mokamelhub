using Application.Common.Dto.Result;
using Application.Services.Language.SeoFieldLangSrv.Dto;
using Application.Services.Language.StoreLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Language.StoreLangSrv.Dto
{
    public class StoreLangSearchDto:BaseSearchDto<SeoFieldLang,SeoFieldLangDto>,IStoreLangSearchFields
    {
        public StoreLangSearchDto(StoreLangInputDto dto,IQueryable<SeoFieldLang> list,IMapper mapper):base(dto,list,mapper)
        {
            StoreId=dto.StoreId;
        }

        public long StoreId { get; set; }
    }
}
