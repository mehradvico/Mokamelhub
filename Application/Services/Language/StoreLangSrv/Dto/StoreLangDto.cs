using Application.Services.Language.SeoFieldLangSrv.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Language.StoreLangSrv.Dto
{
    public class StoreLangDto
    {
        public long StoreId { get; set; }
        public SeoFieldLangDto SeoFieldLangDto { get; set; }
    }
}
