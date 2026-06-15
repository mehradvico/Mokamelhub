using Application.Common.Service;
using Application.Services.CategorySrv.Dto;
using Application.Services.CodeSrv.Dto;
using Application.Services.Setting.CodeSrv.Dto;
using Application.Services.Setting.CodeSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Setting.CodeSrv
{
    public class CodeService : CommonSrv<Code, CodeDto>, ICodeService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public CodeService(IDataBaseContext _context, IMapper mapper) : base(_context: _context, mapper: mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }

        public async Task<CodeDto> GetByLabelAsync(string label)
        {
            var item = await _context.Codes.FirstOrDefaultAsync(s => s.Label.Equals(label));
            return mapper.Map<CodeDto>(item);
        }
        public async Task<CodeDto> GetByIdAsync(long id)
        {
            var item = await _context.Codes.FirstOrDefaultAsync(s => s.Id.Equals(id));
            return mapper.Map<CodeDto>(item);
        }

        public async Task<long> GetIdByLabelAsync(string label)
        {
            var item = await _context.Codes.FirstOrDefaultAsync(s => s.Label.Equals(label));

            return item.Id;

        }

        public CodeSearchDto Search(CodeInputDto baseSearchDto)
        {
            var model = _context.Codes.AsQueryable();
            if (!string.IsNullOrEmpty(baseSearchDto.Q))
            {
                model = model.Where(s => s.Name.Equals(baseSearchDto.Q));
            }
            if (!string.IsNullOrEmpty(baseSearchDto.CodeGroupLabel))
            {
                model = model.Where(s => s.CodeGroup.Label.Equals(baseSearchDto.CodeGroupLabel));
            }
            if (baseSearchDto.Available.HasValue)
            {
                model = model.Where(s => s.Active == baseSearchDto.Available);
            }
            model = model.OrderBy(o => o.Priority);
            return new CodeSearchDto(baseSearchDto, model, mapper);
        }


    }
}
