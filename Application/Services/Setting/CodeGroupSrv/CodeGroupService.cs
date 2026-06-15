using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Service;
using Application.Services.Setting.CodeGroupSrv.Dto;
using Application.Services.Setting.CodeGroupSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Persistence.Interface;
using System.Linq;

namespace Application.Services.Setting.CodeGroupSrv
{
    public class CodeGroupService : CommonSrv<CodeGroup, CodeGroupDto>, ICodeGroupService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;

        public CodeGroupService(IDataBaseContext _context, IMapper mapper) : base(_context: _context, mapper: mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }
        public BaseSearchDto<CodeGroupDto> Search(BaseInputDto baseSearchDto)
        {
            var model = _context.CodeGroups.AsQueryable();
            if (!string.IsNullOrEmpty(baseSearchDto.Q))
            {
                model = model.Where(s => s.Name.Contains(baseSearchDto.Q)).OrderByDescending(o => o.Id);
            }
            return new BaseSearchDto<CodeGroup, CodeGroupDto>(baseSearchDto, model, mapper);
        }


    }
}
