using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.ProductRelateSrv.Dto;
using Application.Services.ProductSrvs.ProductRelateSrv.Iface;
using Application.Services.ProductSrvs.ProductSrv.Dto;
using AutoMapper;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Linq;


namespace Application.Services.ProductSrvs.ProductRelateSrv
{
    public class ProductRelateService : IProductRelateService
    {
        private readonly IDataBaseContext _context;
        private readonly IMapper mapper;
        public ProductRelateService(IDataBaseContext _context, IMapper mapper)
        {
            this._context = _context;
            this.mapper = mapper;
        }


        public BaseResultDto InsertOrUpdate(ProductRelateVDto productRelate)
        {
            var existedItems = _context.ProductRelates.Where(s => s.ProductId == productRelate.ProductId && s.Label == productRelate.Label).ToList();
            var remove = existedItems.Where(s => productRelate.RelatedProductList.Any(a => a.Id == s.RelatedProductId) == false).ToList();
            if (remove.Any())
            {
                existedItems.RemoveAll(s => remove.Any(a => a.Id == s.Id));
                _context.ProductRelates.RemoveRange(remove);

            }
            foreach (var item in productRelate.RelatedProductList)
            {
                if (!existedItems.Any(s => s.RelatedProductId == item.Id))
                {
                    _context.ProductRelates.Add(new ProductRelate() { ProductId = productRelate.ProductId, RelatedProductId = item.Id, Label = productRelate.Label });
                }
            }
            _context.SaveChanges();
            return new BaseResultDto(true);
        }
        public BaseResultDto<ProductRelateVDto> GetForProduct(ProductRelateDto dto)
        {
            var query = _context.ProductRelates.Include(s => s.RelatedProduct).ThenInclude(s => s.Category).Where(s => s.ProductId == dto.ProductId);
            if (!string.IsNullOrEmpty(dto.Label))
            {
                query = query.Where(s => s.Label == dto.Label);
            }
            var model = query.Select(s => s.RelatedProduct);
            var result = new ProductRelateVDto();
            result.ProductId = dto.ProductId;
            result.Label = dto.Label;
            result.RelatedProductList = mapper.Map<List<ProductVDto>>(model);
            return new BaseResultDto<ProductRelateVDto>(isSuccess: true, result);
        }

    }
}
