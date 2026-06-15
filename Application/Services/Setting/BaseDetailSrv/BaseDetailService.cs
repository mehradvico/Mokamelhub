using Application.Common.Dto.Input;
using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Common.Service;
using Application.Services.Setting.BaseDetailSrv.Dto;
using Application.Services.Setting.BaseDetailSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Setting.BaseDetailSrv
{
    public class BaseDetailService : CommonSrv<BaseDetail, BaseDetailDto>, IBaseDetailService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;

        public BaseDetailService(IDataBaseContext _context, IMapper mapper) : base(_context: _context, mapper: mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }
        public async Task<BaseResultDto> GetDtoAsync(string label)
        {
            var item = await _context.BaseDetails.FirstOrDefaultAsync(s => s.Label == label && label != BaseDetailEnum.admin.ToString());
            if (item != null)
                return new BaseResultDto<BaseDetailDto>(true, data: mapper.Map<BaseDetailDto>(item));
            return new BaseResultDto<BaseDetailDto>(false, null);

        }
        public async override Task<BaseResultDto<BaseDetailDto>> InsertAsyncDto(BaseDetailDto dto)
        {
            if (!LabelIsUnique(dto.Label, null))
            {
                return new BaseResultDto<BaseDetailDto>(false, val: Resource.Notification.TheLabelIsDuplicate, dto);
            }
            return await base.InsertAsyncDto(dto);
        }
        public override BaseResultDto UpdateDto(BaseDetailDto dto)
        {
            if (!LabelIsUnique(dto.Label, dto.Id))
            {
                return new BaseResultDto<BaseDetailDto>(false, val: Resource.Notification.TheLabelIsDuplicate, dto);
            }
            return base.UpdateDto(dto);
        }
        public BaseSearchDto<BaseDetailDto> Search(BaseInputDto baseSearchDto)
        {
            var model = _context.BaseDetails.AsQueryable();
            if (!string.IsNullOrEmpty(baseSearchDto.Q))
            {
                model = model.Where(s => s.Label.Contains(baseSearchDto.Q)).OrderByDescending(o => o.Id);
            }
            return new BaseSearchDto<BaseDetail, BaseDetailDto>(baseSearchDto, model, mapper);
        }
        public BaseDetailDto GetBaseDetails()
        {
            var item = _context.BaseDetails.FirstOrDefault(s => s.Label == BaseDetailEnum.admin.ToString());
            return mapper.Map<BaseDetailDto>(item);
        }
        bool LabelIsUnique(string label, long? id)
        {
            var item = _context.BaseDetails.FirstOrDefault(s => s.Label.ToLower() == label.ToLower());
            if (item == null)
                return true;
            else if (item.Id == id)
                return true;
            return false;
        }
    }
}
