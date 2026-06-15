using Application.Common.Dto.Result;
using Application.Common.Enumerable;
using Application.Common.Service;
using Application.Services.ProductSrvs.ProductItemSrv.Dto;
using Application.Services.ProductSrvs.ProductItemSrv.Iface;
using Application.Services.ProductSrvs.ProductSrv.Dto;
using Application.Services.ProductSrvs.ProductSrv.Iface;
using Application.Services.ProductSrvs.VarietyItemSrv.Dto;
using Application.Services.ProductSrvs.VarietySrv.Dto;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Application.Services.ProductSrvs.ProductItemSrv
{
    public class ProductItemService : CommonSrv<ProductItem, ProductItemDto>, IProductItemService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        private readonly IProductService _productService;
        public ProductItemService(IDataBaseContext _context, IMapper mapper, IProductService productService) : base(_context, mapper)
        {
            this._context = _context;
            this.mapper = mapper;
            this._productService = productService;
        }

        public ProductItemSearchDto SearchDto(ProductItemInputDto dto)
        {

            var model = _context.ProductItems.Include(s => s.Store).Include(s => s.VarietyItem).Include(s => s.VarietyItem2).AsQueryable();
            if (dto.ProductId.HasValue)
            {
                model = model.Where(s => s.ProductId.Equals(dto.ProductId));
            }
            if (dto.StoreId.HasValue)
            {
                model = model.Where(s => s.StoreId.Equals(dto.StoreId));
            }
            model = model.OrderByDescending(s => s.ProductId).ThenByDescending(s => s.Quantity);
            return new ProductItemSearchDto(dto, model, mapper);

        }
        public void InsertOrUpdate(ProductItemDto productItem)
        {
            var item = _context.ProductItems.AsTracking().FirstOrDefault(s =>
                ((productItem.Id > 0 && s.Id == productItem.Id) ||
                (productItem.Id == 0 && s.ProductId == productItem.ProductId && s.VarietyItemId == productItem.VarietyItemId && s.VarietyItem2Id == productItem.VarietyItem2Id && s.StoreId == productItem.StoreId))
                && s.Deleted == false);

            if (item != null)
            {
                item.BasePrice = productItem.BasePrice;
                item.WholeSalePrice = productItem.WholeSalePrice;
                item.Price = productItem.BasePrice;
                item.Quantity = productItem.Quantity;
                item.Active = productItem.Active;

                _context.ProductItems.Update(item);
            }
            else
            {
                var newItem = mapper.Map<ProductItem>(productItem);
                newItem.SystemActive = true;

                newItem.VarietyItemId = productItem.VarietyItemId;
                newItem.VarietyItem2Id = productItem.VarietyItem2Id;

                _context.ProductItems.Add(newItem);
            }
            _context.SaveChanges();
        }
        public void InsertOrUpdate(List<ProductItemDto> productItemListUpdate)
        {
            foreach (var item in productItemListUpdate)
            {
                InsertOrUpdate(item);
            }
        }
        public async Task<BaseResultDto> InsertOrUpdateAsync(ProductItemListUpdateDto productItemListUpdate)
        {
            try
            {
                productItemListUpdate.ProductItems.ForEach(s => s.StoreId = productItemListUpdate.StoreId);
                productItemListUpdate.ProductItems.ForEach(s => s.ProductId = productItemListUpdate.ProductId);
                InsertOrUpdate(productItemListUpdate.ProductItems);
                await _productService.UpdateProductPriceAsync(ProductUpdateTypeEnum.Product, Id: productItemListUpdate.ProductId.ToString());
                return new BaseResultDto(true);

            }
            catch
            {
                return new BaseResultDto(false);
            }
        }
        public async Task<BaseResultDto> GetInsertOrUpdateListAsync(ProductItemListRequestDto productItemListrequest)
        {
            ProductItemListUpdateDto productItemListUpdate = new ProductItemListUpdateDto();
            productItemListUpdate.ProductId = productItemListrequest.ProductId;
            productItemListUpdate.StoreId = productItemListrequest.StoreId;

            var product = await _context.Products.AsTracking()
                .Include(s => s.Variety)
                .Include(s => s.Variety2)
                .Include(s => s.ProductItems.Where(p => p.Deleted == false && p.StoreId == productItemListUpdate.StoreId))
                .ThenInclude(s => s.Store)
                .FirstOrDefaultAsync(s => s.Id == productItemListUpdate.ProductId);

            productItemListUpdate.ProductItems = new List<ProductItemDto>();

            if (product != null)
            {
                productItemListUpdate.Product = mapper.Map<ProductMinVDto>(product);

                if (product.ProductItems != null && product.ProductItems.Any())
                {
                    foreach (var item in product.ProductItems)
                    {
                        productItemListUpdate.ProductItems.Add(mapper.Map<ProductItemDto>(item));
                    }
                }
                else
                {
                    productItemListUpdate.ProductItems.Add(new ProductItemDto()
                    {
                        ProductId = productItemListUpdate.ProductId,
                        Active = true,
                        StoreId = productItemListUpdate.StoreId,
                    });
                }
            }

            return new BaseResultDto<ProductItemListUpdateDto>(true, productItemListUpdate);
        }


        public async Task<BaseResultDto<ProductItemVDto>> IsSalable(long productItemId, int count)
        {
            var item = await _context.ProductItems.FirstOrDefaultAsync(x => x.Id == productItemId);
            if (item != null)
            {
                if (item.SystemActive)
                {
                    if (item.Quantity >= count)
                    {
                        return new BaseResultDto<ProductItemVDto>(true, data: mapper.Map<ProductItemVDto>(item));
                    }
                    else
                    {
                        return new BaseResultDto<ProductItemVDto>(false, val: Resource.Notification.InsufficientInventory, null);
                    }
                }
            }
            return new BaseResultDto<ProductItemVDto>(false, val: Resource.Notification.TheProductUnavailable, null);

        }

        public async Task<BaseResultDto> GetVarietyAsync(long productId)
        {
            var product = await _context.Products.AsTracking().Include(s => s.Variety).Include(s => s.Variety2).Include(s => s.ProductItems.OrderByDescending(o => o.SystemActive).ThenBy(s => s.Price)).ThenInclude(s => s.Store).FirstOrDefaultAsync(s => s.Id == productId && s.Status.Label == ProductStatusEnum.ProductStatus_Available.ToString() && s.Active && s.Deleted == false);
            if (product != null && product.ProductItems.Any())
            {
                var result = new ProductItemVarietyVDto();
                result.Variety = mapper.Map<VarietyShowVDto>(product.Variety);
                var group = product.ProductItems.GroupBy(g => g.VarietyItem);
                foreach (var item in group)
                {

                    result.VarietyItems.Add(mapper.Map<VarietyItemShowVDto>(item));
                }
                return new BaseResultDto<ProductItemVarietyVDto>(true, result);
            }
            return new BaseResultDto(false);

        }
        public async Task<BaseResultDto> GetVariety2Async(long productId, long? varietyItem1Id, long? varietyItem2Id)
        {
            var product = await _context.Products.Include(s => s.Variety).Include(s => s.Variety2).FirstOrDefaultAsync(s => s.Id == productId && s.Status.Label == ProductStatusEnum.ProductStatus_Available.ToString() && s.Active && s.Deleted == false);
            if (product != null)
            {
                var result = new ProductItemVariety2VDto();
                result.Variety1 = mapper.Map<VarietyShowVDto>(product.Variety);
                result.Variety2 = mapper.Map<VarietyShowVDto>(product.Variety2);
                result.VarietyItem1Id = varietyItem1Id;
                result.VarietyItem2Id = varietyItem2Id;
                var productItems = _context.ProductItems
                    .Include(s => s.VarietyItem)
                    .Include(s => s.VarietyItem2)
                    .Include(s => s.Store)
                    .Include(s => s.DiscountGroup)
                    .Where(s => !s.Deleted && s.ProductId == productId && s.SystemActive)
                    .OrderByDescending(o => o.SystemActive)
                    .ThenBy(o => o.Price);
                if (await productItems.AnyAsync())
                {
                    if (result.Variety1 != null)
                    {
                        var group = productItems.GroupBy(g => g.VarietyItem);
                        result.VarietyItems1 = mapper.Map<List<VarietyItemMinVDto>>(group.Select(s => s.Key).ToList());
                        if (result.VarietyItem1Id == null)
                            result.VarietyItem1Id = result.VarietyItems1.First().Id;
                        if (result.Variety2 != null)
                        {
                            if (result.VarietyItem2Id == null)
                            {
                                result.VarietyItems2 = mapper.Map<List<VarietyItemMinVDto>>(group.ToList().First().ToList().Select(s => s.VarietyItem2).ToList());
                                result.VarietyItem2Id = result.VarietyItems2.First().Id;
                            }
                            else
                                result.VarietyItems2 = mapper.Map<List<VarietyItemMinVDto>>(group.ToList().First(g => g.Key.Id == varietyItem1Id).ToList().Select(s => s.VarietyItem2).DistinctBy(d => d.Id).ToList());
                        }
                    }
                    result.ProductItems = mapper.Map<List<ProductItemShowVDto>>(productItems.Where(s => s.VarietyItemId == result.VarietyItem1Id && s.VarietyItem2Id == result.VarietyItem2Id));
                    return new BaseResultDto<ProductItemVariety2VDto>(true, result);
                }
            }

            return new BaseResultDto(false);

        }

        public async Task<BaseResultDto<ProductItemForProductDto>> GetForProductVDto(long productId)
        {

            var allitems = await _context.Products.IgnoreAutoIncludes().Include(s => s.Variety).Include(s => s.Variety2).Include(s => s.ProductItems.Where(s => s.ProductId == productId && s.Active && s.SystemActive && s.Deleted == false)).ThenInclude(s => s.Store).ThenInclude(s => s.Type).Include(s => s.ProductItems).ThenInclude(s => s.VarietyItem).Include(s => s.ProductItems).ThenInclude(s => s.VarietyItem2).FirstOrDefaultAsync(s => s.Id == productId);
            return new BaseResultDto<ProductItemForProductDto>(true, mapper.Map<ProductItemForProductDto>(allitems));
        }
    }
}
