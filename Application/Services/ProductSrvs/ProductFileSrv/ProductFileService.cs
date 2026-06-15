using Application.Common.Dto.Result;
using Application.Common.Service;
using Application.Services.Accounting.UserProductSrv.Iface;
using Application.Services.ProductSrvs.ProductFileSrv.Dto;
using Application.Services.ProductSrvs.ProductFileSrv.Iface;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.ProductFileSrv
{
    public class ProductFileService : CommonSrv<ProductFile, ProductFileDto>, IProductFileService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly IUserProductService _userProductService;
        public ProductFileService(IDataBaseContext _context, IMapper mapper, IUserProductService _userProductService) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
            this._userProductService = _userProductService;
        }

        public ProductFileSearchDto SearchDto(ProductFileInputDto dto)
        {
            var model = _context.ProductFiles.Include(s => s.File).Include(s => s.Children.Where(a => a.Deleted == false)).ThenInclude(s => s.File).Where(s => s.Deleted == false).AsQueryable();
            if (dto.ProductId.HasValue)
            {
                model = model.Where(s => s.ProductId.Equals(dto.ProductId));
            }
            model = model.Where(s => s.ParentId.Equals(dto.ParentId));
            if (!string.IsNullOrEmpty(dto.Q))
            {
                model = model.Where(s => s.Label.Contains(dto.Q));
            }
            return new ProductFileSearchDto(dto, model, mapper);
        }
        public async Task<BaseResultDto> GetForUserAsync(long productId, long? userId)
        {

            var model = _context.ProductFiles.Include(s => s.File).Include(s => s.Children.Where(a => a.Deleted == false)).ThenInclude(s => s.File).Where(s => s.ProductId == productId && s.Deleted == false).AsQueryable();
            if (userId.HasValue)
            {
                if (await _userProductService.UserHasProductAsync(productId, userId.Value))
                {
                    return new BaseResultDto<List<ProductFileVDto>>(true, mapper.Map<List<ProductFileVDto>>(model));
                }
            }
            var items = mapper.Map<List<ProductFileVDto>>(model);
            foreach (var item in items.Where(s => s.Protected))
            {
                item.File = null;
                foreach (var c in item.Children)
                {
                    c.File = null;
                }
            }
            return new BaseResultDto<List<ProductFileVDto>>(false, items);
        }
    }
}
