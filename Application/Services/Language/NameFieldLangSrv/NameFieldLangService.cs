using Application.Common.Service;
using Application.Services.Language.NameFieldLangSrv.Dto;
using Application.Services.Language.NameFieldLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Persistence.Interface;

namespace Application.Services.Language.NameFieldLangSrv
{
    public class NameFieldLangService : CommonSrv<NameFieldLang, NameFieldLangDto>, INameFieldLangService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public NameFieldLangService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }
    }
}
