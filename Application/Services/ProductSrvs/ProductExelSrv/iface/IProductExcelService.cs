using Application.Common.Dto.Result;
using Application.Services.ProductSrvs.ProductExelSrv.Dto;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.ProductExelSrv.iface
{
    public interface IProductExcelService
    {
        MemoryStream GetProductExcelTemplate();
        Task<MemoryStream> ImportProductsAsync(IFormFile file);
    }
}
