using Application.Services.ProductSrvs.ProductCategorySrv.Iface;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.ProductSrv.ProductCategorySrv
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IDataBaseContext _context;

        public ProductCategoryService(IDataBaseContext _context)
        {
            this._context = _context;
        }

        public async Task InsertOrUpdateAsync(Product product, long categoryId)
        {
            var item = await _context.Categories.AsTracking().FirstOrDefaultAsync(s => s.Id == categoryId);
            if (item != null)
            {
                product.Categories.Add(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task InsertOrUpdateAsync(Product product, List<long> categoryIds)
        {
            if (product.Categories != null)
            {
                product.Categories.Clear();
                await _context.SaveChangesAsync();
            }
            else
            {
                product.Categories = new List<Category>();
            }
            foreach (var item in categoryIds)
            {
                await InsertOrUpdateAsync(product, item);
            }
        }
    }
}
