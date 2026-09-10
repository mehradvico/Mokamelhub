using Application.Common.Enumerable;
using Hangfire.Dashboard;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Api.HangFire
{
    /// <summary>
    /// The Hangfire dashboard bypasses MVC routing, so [Authorize]/the area-based
    /// permission check in OnTokenValidatedService never runs for it. This filter
    /// re-checks the same JWT bearer token for an Admin RoleId claim, and also
    /// allows the server's own loopback address (e.g. an SSH tunnel) as a fallback
    /// for local operational access when no token is presented from a browser tab.
    /// </summary>
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();

            var roleClaim = httpContext.User?.FindFirst("RoleId")?.Value;
            if (httpContext.User?.Identity?.IsAuthenticated == true &&
                long.TryParse(roleClaim, out var roleId) &&
                roleId == (long)RoleEnum.Admin)
            {
                return true;
            }

            return httpContext.Connection.RemoteIpAddress != null &&
                   IPAddress.IsLoopback(httpContext.Connection.RemoteIpAddress);
        }
    }
}
