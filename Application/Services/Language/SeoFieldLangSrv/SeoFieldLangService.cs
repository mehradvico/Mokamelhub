using Application.Common.Service;
using Application.Services.Language.SeoFieldLangSrv.Dto;
using Application.Services.Language.SeoFieldLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Persistence.Interface;

namespace Application.Services.Language.SeoFieldLangSrv
{
    public class SeoFieldLangService : CommonSrv<SeoFieldLang, SeoFieldLangDto>, ISeoFieldLangService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;

        public SeoFieldLangService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;

        }

    }
}
