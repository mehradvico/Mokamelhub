using Application.Common.Dto.Input;
using Application.Services.Language.StoreLangSrv.Iface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Language.StoreLangSrv.Dto
{
    public class StoreLangInputDto : BaseInputDto, IStoreLangSearchFields
    {
        public long StoreId { get; set; }
    }
}
