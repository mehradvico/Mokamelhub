using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ProductSrvs.TorobSrv.Dto
{
    public sealed class TorobServiceResult
    {
        public bool IsSuccess { get; private set; }

        public string Error { get; private set; }

        public TorobResponseDto Data { get; private set; }

        public static TorobServiceResult Success(TorobResponseDto data)
        {
            return new TorobServiceResult
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static TorobServiceResult Failure(string error)
        {
            return new TorobServiceResult
            {
                IsSuccess = false,
                Error = error
            };
        }
    }
}
