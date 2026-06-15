using Application.Common.Service;
using Application.Services.Language.FullNameFieldLangSrv.Dto;
using Application.Services.Language.FullNameFieldLangSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Persistence.Interface;

namespace Application.Services.Language.FullNameFieldLangSrv
{
    public class FullNameFieldLangService : CommonSrv<FullNameFieldLang, FullNameFieldLangDto>, IFullNameFieldLangService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public FullNameFieldLangService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }
    }
}
