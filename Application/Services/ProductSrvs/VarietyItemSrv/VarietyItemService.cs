using Application.Common.Dto.Result;
using Application.Common.Helpers;
using Application.Common.Service;
using Application.Services.ProductSrvs.VarietyItemSrv.Dto;
using Application.Services.ProductSrvs.VarietyItemSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.VarietyItemSrv
{
    public class VarietyItemService : CommonSrv<VarietyItem, VarietyItemDto>, IVarietyItemService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public VarietyItemService(IDataBaseContext _context, IMapper mapper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }

        public VarietyItemSearchDto SearchDto(VarietyItemInputDto searchDto)
        {
            var model = _context.VarietyItems.Where(s => s.VarietyId == searchDto.VarietyId && s.Deleted == false).AsQueryable();
            if (!string.IsNullOrEmpty(searchDto.Q))
            {
                model = model.Where(s => s.Name.Contains(searchDto.Q)).OrderByDescending(o => o.Id);
            }
            return new VarietyItemSearchDto(searchDto, model, mapper);
        }

        public override async Task<BaseResultDto<VarietyItemDto>> InsertAsyncDto(VarietyItemDto dto)
        {

            try
            {
                var modelCheker = ModelHelper<VarietyItemDto>.ModelErrors(dto);
                if (!modelCheker.IsSuccess)
                {
                    return modelCheker;
                }
                else
                {
                    if ((!NameIsUnique(dto.Name)))
                    {
                        return new BaseResultDto<VarietyItemDto>(isSuccess: false, val1: Resource.Notification.TheNameIsDuplicate, val2: nameof(dto.Name), dto);
                    }
                    var item = mapper.Map<VarietyItem>(dto);
                    await _context.VarietyItems.AddAsync(item);
                    _context.SaveChanges();
                    return new BaseResultDto<VarietyItemDto>(true, mapper.Map<VarietyItemDto>(item));
                }

            }
            catch (Exception ex)
            {
                return new BaseResultDto<VarietyItemDto>(isSuccess: false, val: ex.Message, data: dto);
            }


        }

        public override BaseResultDto UpdateDto(VarietyItemDto dto)
        {

            try
            {
                var modelCheker = ModelHelper<VarietyItemDto>.ModelErrors(dto);
                if (!modelCheker.IsSuccess)
                {
                    return modelCheker;
                }
                else
                {
                    var item = _context.VarietyItems.FirstOrDefault(s => s.Id == dto.Id);
                    if (dto.Name != item.Name && (!NameIsUnique(dto.Name)))
                    {
                        return new BaseResultDto<VarietyItemDto>(isSuccess: false, val1: Resource.Notification.TheNameIsDuplicate, val2: nameof(dto.Name), dto);
                    }
                    mapper.Map(dto, item);
                    _context.VarietyItems.Attach(item);
                    _context.Entry(item).State = EntityState.Modified;
                    _context.SaveChanges();
                    return new BaseResultDto<VarietyItemDto>(true, mapper.Map<VarietyItemDto>(item));
                }
            }
            catch (Exception ex)
            {
                return new BaseResultDto(isSuccess: false, val: ex.Message);
            }

        }
        bool NameIsUnique(string name)
        {
            var item = _context.VarietyItems.FirstOrDefault(x => x.Name == name);
            if (item == null)
                return true;
            return false;

        }
    }
}
