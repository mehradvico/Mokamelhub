using Application.Services.ProductSrvs.TorobSrv.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.TorobSrv.Iface
{
    public interface ITorobService
    {
        Task<TorobServiceResult> GetProductsAsync(TorobRequestDto request, CancellationToken cancellationToken = default);
    }
}
