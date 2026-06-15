using Microsoft.AspNetCore.Http;
using System;


namespace Application.Common.Helpers
{
    public static class HttpContextHelper
    {
        public static long GetStoreId(this HttpContext httpContext)
        {
            var storeId = Convert.ToInt32(httpContext.Request.Headers["storeid"]);
            return storeId;
        }

    }
}
