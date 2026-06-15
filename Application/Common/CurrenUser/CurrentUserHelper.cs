using Application.Common.Interface;
using Application.Services.Accounting.UserSrv.Iface;
using Application.Services.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Application.Common.CurrentUser
{

    public class CurrentUserHelper : ICurrentUserHelper
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserHelper(IUserService userService, IHttpContextAccessor httpContext)
        {
            _httpContextAccessor = httpContext;
            _userService = userService;
        }
        public CurrentUserDto CurrentUser
        {
            get
            {
                if (_httpContextAccessor.HttpContext != null)
                {
                    var token = _httpContextAccessor?.HttpContext?.GetTokenAsync("access_token").Result;
                    return _userService.GetByTokenDto(token).Result;
                }
                return null;

            }
        }
    }
}
