using Application.Common.Dto.Result;
using Application.Common.Helpers;
using Application.Common.Interface;
using Application.Common.Service;
using Application.Services.Order.AddressSrv.Dto;
using Application.Services.Order.AddressSrv.iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Content.AddressSrv
{
    public class AddressService : CommonSrv<Address, AddressDto>, IAddressService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly ICurrentUserHelper _currentUserHelper;
        public AddressService(IDataBaseContext _context, IMapper mapper, ICurrentUserHelper currentUserHelper) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
            this._currentUserHelper = currentUserHelper;
        }
        public override Task<BaseResultDto<AddressDto>> InsertAsyncDto(AddressDto dto)
        {
            dto.UserId = _currentUserHelper.CurrentUser.UserId;
            return base.InsertAsyncDto(dto);
        }

        public override async Task<BaseResultDto<AddressDto>> FindAsyncDto(long id)
        {
            var currentUserId = _currentUserHelper.CurrentUser.UserId;
            var item = await _context.Addresses
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == currentUserId && !s.Deleted);

            return item == null
                ? new BaseResultDto<AddressDto>(false, data: null)
                : new BaseResultDto<AddressDto>(true, mapper.Map<AddressDto>(item));
        }

        public override BaseResultDto UpdateDto(AddressDto dto)
        {
            try
            {
                var currentUserId = _currentUserHelper.CurrentUser.UserId;
                if (!_context.Addresses.Any(s =>
                    s.Id == dto.Id &&
                    s.UserId == currentUserId &&
                    !s.Deleted))
                {
                    return new BaseResultDto(false, "آدرس انتخاب‌شده معتبر نیست.");
                }

                dto.UserId = currentUserId;
                var modelCheker = ModelHelper<AddressDto>.ModelErrors(dto);
                if (!modelCheker.IsSuccess)
                {
                    return modelCheker;
                }
                else
                {
                    long addressId = dto.Id;
                    dto.Id = 0;
                    var newAddress = InsertAsyncDto(dto).Result;
                    _context.ProductOrders.Where(s => s.AddressId == addressId).ExecuteUpdate(s => s.SetProperty(x => x.AddressId, newAddress.Data.Id));
                    DeleteDto(newAddress.Data);
                    dto.Id = addressId;


                    var item = mapper.Map<Address>(dto);
                    item.Deleted = false;
                    _context.Addresses.Attach(item);
                    _context.Entry(item).State = EntityState.Modified;

                    _context.SaveChanges();
                    return new BaseResultDto(isSuccess: true);
                }
            }
            catch (Exception ex)
            {
                return new BaseResultDto(isSuccess: false, val: ex.Message);
            }
        }

        public override BaseResultDto DeleteDto(long id)
        {
            var currentUserId = _currentUserHelper.CurrentUser.UserId;
            var item = _context.Addresses.FirstOrDefault(s =>
                s.Id == id &&
                s.UserId == currentUserId &&
                !s.Deleted);

            if (item == null)
                return new BaseResultDto(false, "آدرس انتخاب‌شده معتبر نیست.");

            item.Deleted = true;
            _context.Addresses.Update(item);
            _context.SaveChanges();
            return new BaseResultDto(true);
        }

        public AddressSearchDto Search(AddressInputDto searchDto)
        {
            var query = _context.Addresses.Where(s => !s.Deleted).AsQueryable();
            query = query.Where(s => s.UserId == searchDto.UserId);
            if (searchDto.SortBy != Common.Enumerable.SortEnum.Default)
            {
                switch (searchDto.SortBy)
                {
                    case Common.Enumerable.SortEnum.Default:
                        {
                            query = query.OrderByDescending(s => s.Id);
                            break;
                        }
                    case Common.Enumerable.SortEnum.New:
                        {
                            query = query.OrderByDescending(s => s.Id);
                            break;
                        }
                    case Common.Enumerable.SortEnum.Old:
                        {
                            query = query.OrderBy(s => s.Id);
                            break;
                        }
                    default:
                        break;
                }
            }

            return new AddressSearchDto(searchDto, query, mapper);
        }
    }
}
