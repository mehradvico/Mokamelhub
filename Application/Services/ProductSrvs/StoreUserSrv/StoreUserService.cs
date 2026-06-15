using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.StoreSrv.Dto;
using Application.Services.ProductSrvs.StoreUserSrv.Dto;
using Application.Services.StoreSrvs.StoreUserSrv.Iface;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.StoreSrv.StoreUserSrv
{
    public class StoreUserService : IStoreUserService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;

        public StoreUserService(IDataBaseContext _context, IMapper mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }
        public async Task<BaseResultDto> GetAllAsync(StoreUserDto storeUser)
        {
            if (storeUser.UserId > 0)
            {
                var user = await _context.Users.Include(s => s.Stores.Where(a => storeUser.Active.HasValue ? a.Active == storeUser.Active : true && !a.Deleted)).ThenInclude(s => s.Picture).AsTracking().FirstOrDefaultAsync(s => s.Id == storeUser.UserId);
                if (user != null && user.Stores.Any())
                {
                    return new BaseResultDto<List<StoreMinVDto>>(true, data: mapper.Map<List<StoreMinVDto>>(user.Stores));
                }
            }
            else if (storeUser.StoreId > 0)
            {
                var store = await _context.Stores.Include(s => s.Users.Where(a => storeUser.Active.HasValue ? a.Locked == (!storeUser.Active) : true && !s.Deleted)).AsTracking().FirstOrDefaultAsync(s => s.Id == storeUser.StoreId);
                if (store != null && store.Users.Any())
                {
                    return new BaseResultDto<List<StoreMinVDto>>(true, data: mapper.Map<List<StoreMinVDto>>(store.Users));
                }
            }
            return new BaseResultDto(false);
        }
        public async Task<BaseResultDto> InsertAsync(StoreUserDto storeUser)
        {
            try
            {
                var user = await _context.Users.Include(s => s.Stores).AsTracking().FirstOrDefaultAsync(s => s.Id == storeUser.UserId);
                var store = await _context.Stores.Include(s => s.Users).AsTracking().FirstOrDefaultAsync(s => s.Id == storeUser.StoreId);
                if (user != null && store != null)
                {
                    store.Users.Add(user);
                    await _context.SaveChangesAsync();
                }
                return new BaseResultDto(true);
            }
            catch
            {
                return new BaseResultDto(false, val: Resource.Notification.Unsuccess);
            }
        }
        public async Task<BaseResultDto> RemoveAsync(StoreUserDto storeUser)
        {
            try
            {
                var user = await _context.Users.Include(s => s.Stores).AsTracking().FirstOrDefaultAsync(s => s.Id == storeUser.UserId);
                var store = await _context.Stores.Include(s => s.Users).AsTracking().FirstOrDefaultAsync(s => s.Id == storeUser.StoreId);
                if (user != null && store != null)
                {
                    store.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
                return new BaseResultDto(true);
            }
            catch
            {
                return new BaseResultDto(false, val: Resource.Notification.Unsuccess);
            }
        }



    }
}
