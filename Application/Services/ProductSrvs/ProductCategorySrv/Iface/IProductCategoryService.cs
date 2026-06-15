using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.ProductCategorySrv.Iface
{
    public interface IProductCategoryService
    {
        Task InsertOrUpdateAsync(Product product, long categoryId);
        Task InsertOrUpdateAsync(Product product, List<long> categoryIds);
    }
}
